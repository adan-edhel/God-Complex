using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputProcessor : MonoBehaviour
{
    private GameObject character;

    IThirdPersonFunctions TPFunctions;

    public bool ToggleRun = true;
    
    [SerializeField] private GameObject playerCharacterPrefab;
    [SerializeField] private GameObject cameraRigPrefab;

    private void Start()
    {
        if (Camera.main) Destroy(Camera.main.gameObject);

        character = Instantiate(playerCharacterPrefab);
        character.GetComponent<Character>().playerInput = this;

        Instantiate(cameraRigPrefab);
        CameraController.Instance.AssignTargets(character);

        TPFunctions = character.GetComponent<IThirdPersonFunctions>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        TPFunctions?.moveInput(ctx.ReadValue<Vector2>());

        if (ctx.canceled)
        {
            TPFunctions?.moveInput(Vector2.zero);
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            TPFunctions?.Jump();
        }
    }
    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            TPFunctions?.Shoot();
        }
    }

    public void OnReload(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            TPFunctions?.Reload();
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            TPFunctions?.Interact();
        }
    }

    public void OnFreelook(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            //TPFunctions?.Freelook(true);
            CameraController.Instance.rotatingCamera = true;
        }
        if (ctx.canceled)
        {
            //TPFunctions?.Freelook(false);
            CameraController.Instance.rotatingCamera = false;
        }
    }

    public void OnAimDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            TPFunctions?.AimDown(true);
        }
        if (ctx.canceled)
        {
            TPFunctions?.AimDown(false);
        }
    }

    public void OnToggleRun(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ToggleRun = !ToggleRun;
        }
    }
}
