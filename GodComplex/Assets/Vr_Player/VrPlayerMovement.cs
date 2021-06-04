using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VrPlayerMovement : MonoBehaviour
{
    private CharacterController character;
    public static XRController climbingHand;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (climbingHand)
        {
            //gravity uit
            Climb();
        }
        else
        {
            //gravity aan
        }
    }

    private void Climb()
    {
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

        character.Move(transform.rotation * -velocity * Time.fixedDeltaTime);

        // initially, the temporary vector should equal the player's position
        Vector3 clampedPosition = transform.position;
        // Now we can manipulte it to clamp the y element
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -0, 4.1f);
        // re-assigning the transform's position will clamp it
        transform.position = clampedPosition;
    }
}
