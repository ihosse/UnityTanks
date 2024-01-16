using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
    private new Rigidbody rigidbody;
    private float speed;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rigidbody.velocity = transform.forward * speed * 10;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
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
