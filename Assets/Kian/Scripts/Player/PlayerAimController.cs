using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimController : MonoBehaviour
{
    private Vector2 aimDir;

    private void Update()
    {
        Vector2 direction = Vector2.down * aimDir.x + Vector2.right * aimDir.y;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void Aim(InputAction.CallbackContext context)
    {
        aimDir = context.ReadValue<Vector2>();
    }
}
