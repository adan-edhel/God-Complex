using UnityEngine;

public interface IThirdPersonFunctions
{
    void moveInput(Vector2 value);

    void Jump();

    void Shoot();

    void Reload();

    void Interact();

    bool Freelook(bool toggle);

    bool AimDown(bool toggle);
}
