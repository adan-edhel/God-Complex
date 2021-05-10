using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, IThirdPersonFunctions
{
    private Vector3 movement;

    private Animator animator;

    private Character character;

    [SerializeField] float walkSpeed = 2.5f;
    [SerializeField] float runSpeed = 5f;

    public LayerMask Terrain;

    // Interface functions

    private void Start()
    {
        character = GetComponent<Character>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (movement.magnitude > 0)
        {
            float currentSpeed;
            if (character.playerInput.ToggleRun) currentSpeed = runSpeed;
            else currentSpeed = walkSpeed;

            movement.Normalize();
            movement *= currentSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }

        float velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        float velocityX = Vector3.Dot(movement.normalized, transform.right);

        animator.SetBool("ToggleRun", character.playerInput.ToggleRun);
        animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }

    public void moveInput(Vector2 value)
    {
        movement = new Vector3(value.x, 0, value.y);
    }
    public void Jump()
    {
        
    }
    public void Shoot()
    {
        
    }
    public void Reload()
    {
        
    }
    public void Interact()
    {
        
    }
    public bool Freelook(bool toggle)
    {
        return toggle;
    }
    public bool AimDown(bool toggle)
    {
        return toggle;
    }
}
