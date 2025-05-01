using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class FPSLook : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Sensitivity of the camera movement.")]
    public float sensitivity = 1;
    [Header("Camera Limits")]
    [Tooltip("Lowest angle camera can tilt.")]
    public float xRotMin = -89;
    [Tooltip("Highest angle camera can tilt.")]
    public float xRotMax = 89;
    [Header("Unity Set Up")]
    public Transform cam;

    private float _xRotation = 0;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DoLook(InputAction.CallbackContext context)
    {
        // grab input value
        Vector2 input = context.ReadValue<Vector2>();
        // factor in sensitivity
        input *= sensitivity;

        // Horizontal turn
        transform.Rotate(Vector3.up, input.x);

        // Vertical turn
        _xRotation -= input.y;
        _xRotation = Mathf.Clamp(_xRotation, xRotMin, xRotMax);
        cam.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(_xRotation, 0, 0));
    }
}
