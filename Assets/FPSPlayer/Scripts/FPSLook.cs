using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class FPSLook : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Sensitivity of the camera movement")]
    public float Sensitivity = 1;
    [Header("Camera Limits")]
    public float xRotMin = -89;
    public float xRotMax = 89;
    [Header("Unity Set Up")]
    public Transform Cam;

    private float xRotation = 0;

    // Input System boilerplate
    private InputSystem_Actions actions;
    private InputAction look;

    private void Awake() 
    {
        // Input System boilerplate
        actions = new();
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        DoLook();
    }

    private void DoLook()
    {
        // Reading input
        Vector2 input = look.ReadValue<Vector2>();
        input *= Sensitivity;

        // Horizontal turn
        transform.Rotate(Vector3.up, input.x);

        // Vertical turn
        xRotation -= input.y;
        xRotation = Mathf.Clamp(xRotation, xRotMin, xRotMax);
        Cam.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(xRotation, 0, 0));
    }

    private void OnEnable()
    {
        // Input System boilerplate
        look = actions.Player.Look;
        look.Enable();
    }

    private void OnDisable()
    {
        // Input System boilerplate
        look.Disable();
    }
}
