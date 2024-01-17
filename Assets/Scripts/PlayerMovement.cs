using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public bool CanMove { get; set; }

    private PlayerInput playerInput;
    private new Rigidbody rigidbody;
    private float speed;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!CanMove)
            rigidbody.velocity = 10 * speed * transform.forward;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 rawDirection = context.ReadValue<Vector2>();
            Vector3 direction = new Vector3(-rawDirection.x, 0, -rawDirection.y);

            transform.forward = direction;
        }
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        speed = context.ReadValue<float>();
    }
}
