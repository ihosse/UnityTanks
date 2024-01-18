using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody), typeof(PlayerSound))]
public class PlayerMovement : MonoBehaviour
{
    public bool CanMove { get; set; }

    private PlayerInput playerInput;
    private new Rigidbody rigidbody;
    private PlayerSound playerSound;

    private float speed;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
        playerSound = GetComponent<PlayerSound>();
    }

    private void Update()
    {
        if (!CanMove)
            return;

        rigidbody.velocity = 10 * speed * transform.forward;

        playerSound.ChangeEngineSound(speed > 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!CanMove)
            return;

        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 rawDirection = context.ReadValue<Vector2>();

            float playerForward = rawDirection.y;
            float playerRight = rawDirection.x;

            Vector3 direction = DirectionRelativeToCamera(playerForward, playerRight);

            transform.forward = direction;
        }
    }

    public Vector3 DirectionRelativeToCamera(float playerForward, float playerRight)
    {
        Vector3 cameraForward = Camera.main.transform.forward.normalized;
        Vector3 cameraRight = Camera.main.transform.right.normalized;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 forwardRelativeToCamera = playerForward * cameraForward;
        Vector3 rightRelativeToCamera = playerRight * cameraRight;

        return forwardRelativeToCamera + rightRelativeToCamera;
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        speed = context.ReadValue<float>();
    }
}
