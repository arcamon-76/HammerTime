using UnityEngine;
using UnityEngine.InputSystem;

public class Playerflipper : MonoBehaviour
{
    public void OnMoveInput(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (callback.ReadValue<float>() <= 0)
            {
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
                transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
