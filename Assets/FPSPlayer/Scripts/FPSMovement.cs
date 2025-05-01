using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class FPSMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Player speed in units/second.")]
    public float speed;
    [Header("Gravity Settings")]
    [Tooltip("Force of gravity.\nNegative values correspond to a downward force.\nPositive values correspond to an upward force.\nSet to 0 to effectively disable gravity.")]
    public float gravity;
    [Tooltip("Force applied to settle into the ground\nNegative values correspond to a downward force.\nPositive values correspond to an upward force.\nSet to 0 to effectively disable settling force.")]
    public float settling;
    [Header("Jump Settings")]
    [Tooltip("Height of jump.\nSet to zero to effectively disable jumping.")]
    public float jumpHeight;

    private CharacterController _controller;

    private InputSystem_Actions _actions;
    private InputAction _moveAction;

    private float _yVelocity;

    void Awake()
    {
        _actions = new();
        _controller = GetComponent<CharacterController>();

        _yVelocity = 0;
    }

    void Update()
    {
        HorizontalMove();

        Gravity();

        VerticalMove();
    }

    private void HorizontalMove(){
        // get input value
        Vector2 input = _moveAction.ReadValue<Vector2>();
        // convert to horizontal movement based on player facing direction
        Vector3 move = transform.forward * input.y + transform.right * input.x;
        // apply movement
        _controller.Move(speed * Time.deltaTime * move);
    }

    private void Gravity(){
        // settle into the ground
        if(_controller.isGrounded){
            _yVelocity = settling;
            return;
        }

        // add gravity;
        _yVelocity += gravity * Time.deltaTime;
    }

    private void VerticalMove(){
        _controller.Move(_yVelocity * Time.deltaTime * Vector3.up);
    }

    public void Jump(InputAction.CallbackContext context){
        // do nothing if not grounded
        if(!_controller.isGrounded){
            return;
        }

        _yVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    void OnEnable()
    {
        _moveAction = _actions.Player.Move;
        _moveAction.Enable();
    }

    void OnDisable()
    {
        _moveAction.Disable();
    }
}
