using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    public LayerMask groundLayer; // Layer for the ground
    public Transform groundCheck; // Position to check for ground
    public float groundCheckRadius = 0.2f; // Radius of the ground check

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Get movement input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.Normalize(); // Prevent faster diagonal movement

        // Jumping (only if grounded)
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement while keeping the existing Y velocity
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }
}
