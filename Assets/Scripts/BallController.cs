using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveForce = 10f;
    public float maxSpeed = 9f;

    [Header("Jump Settings")]
    public float jumpForce = 5f;
    public float coyoteTime = 0.12f;

    private Rigidbody rb;
    private float radius;
    private float lastGroundedTime = -10f;
    private bool jumpRequested = false;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        radius = 0.5f * transform.lossyScale.x;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, radius + 0.15f,
                               ~0, QueryTriggerInteraction.Ignore);
    }

    void Update()
    {
        Keyboard k = Keyboard.current;
        if (k == null) return;

        float x = 0f;
        float y = 0f;
        if (k.aKey.isPressed || k.leftArrowKey.isPressed) x -= 1f;
        if (k.dKey.isPressed || k.rightArrowKey.isPressed) x += 1f;
        if (k.sKey.isPressed || k.downArrowKey.isPressed) y -= 1f;
        if (k.wKey.isPressed || k.upArrowKey.isPressed) y += 1f;
        moveInput = new Vector2(x, y);

        if (IsGrounded()) lastGroundedTime = Time.time;

        if (k.spaceKey.wasPressedThisFrame) jumpRequested = true;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
        rb.AddForce(movement * moveForce);

        Vector3 flat = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flat.magnitude > maxSpeed)
        {
            Vector3 limited = flat.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limited.x, rb.linearVelocity.y, limited.z);
        }

        if (jumpRequested)
        {
            jumpRequested = false;
            if (Time.time - lastGroundedTime <= coyoteTime)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                lastGroundedTime = -10f;
                AudioManager.Play(Sfx.Jump, 0.8f);
            }
        }
    }
}