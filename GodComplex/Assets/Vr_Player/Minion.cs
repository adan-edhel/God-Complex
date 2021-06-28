using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    private void Update()
    {
        agent.destination = FindTarget().transform.position;
    }

    // This list holds all of the targets that are in range
    private List<Transform> targets = new List<Transform>();

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        targets.Add(other.transform);
    }

    void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.transform)){
            targets.Remove(other.transform);
        }
    }

    private Transform FindTarget()
    {

        // First job is to see if there is anything in the list
        // No point running expensive code if nothing is in range
        if (targets.Count <= 0)
        {
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
}
