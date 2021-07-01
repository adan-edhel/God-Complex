using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject goal;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectileStart;
    public GameObject muzzle;
    private LineRenderer laserLine;
    public bool attacking = false;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.1f);
    public float fireRate = 0.25f;
    private float nextFire;

    private Vector3 differenceVector;

    public float health = 200f;
    public int minDmg;
    public int maxDmg;

    private void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    private void Update()
    {
        if (FindTarget())
        {
            agent.destination = FindTarget().transform.position;
        }
        else
        {
            agent.destination = goal.transform.position;
        }

        if (attacking)
        {
            Attack();
        }
    }

    // This list holds all of the targets that are in range
    private List<Transform> targets = new List<Transform>();

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Minion")
        {
            targets.Add(other.transform);
            attacking = true;
        }
    }

    private void Attack()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        if (FindTarget())
        {
            Vector3 difference = FindTarget().transform.position - transform.position;
            float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
            projectileStart.transform.LookAt(FindTarget().transform.position);
        }

        ///Attack code here
        /// // Update the time when our player can fire next
        if (Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            // Start our ShotEffect coroutine to turn our laser line on and off
            StartCoroutine(ShotEffect());

            // Start our ShotEffect coroutine to turn our laser line on and off

            // Create a vector at the center of our camera's viewport
            Vector3 rayOrigin = projectileStart.transform.position;

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Set the start position for our visual effect for our laser to the position of gunEnd
            laserLine.SetPosition(0, projectileStart.transform.position);

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, projectileStart.transform.forward, out hit, 70f))
            {
                // Set the end position for our laser line 
                Debug.Log(hit.transform.gameObject);
                laserLine.SetPosition(1, hit.point);
                if(hit.transform.gameObject.tag == "Minion")
                {
                    hit.transform.gameObject.GetComponent<Minion>().TakeDamage(Random.Range(minDmg, maxDmg));
                }
            }
            else
            {
                // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                if (hit.transform.gameObject.tag == "Minion")
                {
                    laserLine.SetPosition(1, hit.transform.position);
                }
                else
                {
                    laserLine.SetPosition(1, rayOrigin + (projectileStart.transform.forward * 70f));
                }
            }
        }
        ///End of attack code
    }

    void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.transform))
        {
            targets.Remove(other.transform);
        }
    }

    private Transform FindTarget()
    {

        // First job is to see if there is anything in the list
        // No point running expensive code if nothing is in range
        if (targets.Count <= 0)
        {
            attacking = false;
            return null;
        }

        // Now we remove everything that is null from the list
        // Null transforms get on the list when a target is in range and destroyed
        // This is called a lambda function. Don't ask how it works, but it does.
        targets.RemoveAll(item => item == null);

        // And then we check again if there are any items left
        if (targets.Count <= 0)
        {
            return null;
        }

        // Now we check each remaining target in turn to see which one we should be aiming at
        // The code will check each possible target in turn to calculate a score
        // The score will be compared to the current best score
        // We will store the best target and score in bestTarget and bestScore
        Transform bestTarget = null;
        float bestScore = -999999;
        foreach (Transform currentTarget in targets)
        {
            float currentScore = ScoreTarget(currentTarget);
            if (currentScore > bestScore)
            {
                bestTarget = currentTarget;
                bestScore = currentScore;
            }
        }

        // Now our only job is to return the target we found
        return bestTarget;
    }

    // This method is used to score the targets
    // My implementation will just check the distance from the entity, closer targets get a higher score
    // However you can make this as complex as you like
    private float ScoreTarget(Transform target)
    {
        float score = 0;
        score = 100 - (target.position - transform.position).sqrMagnitude;
        return score;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }


    private IEnumerator ShotEffect()
    {
        // Play the shooting sound effect

        // Turn on our line renderer
        laserLine.enabled = true;

        //Wait for .07 seconds
        yield return shotDuration;

        // Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }

    private void ShootEffect()
    {
        if (attacking && alreadyAttacked == false)
        {
            laserLine.enabled = true;
        }
        else
        {
            laserLine.enabled = false;
        }
    }

}
