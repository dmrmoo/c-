using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 8f;
    public float jumpSpeed = 7f;

    // To keep our rigid body
    private Rigidbody2D rb;

    // To keep the collider object
    private Collider2D coll;

    // Flag to keep track of whether a jump started
    private bool pressedJump = false;

    // Use this for initialization
    void Start()
    {
        // Get the rigid body component for later use
        rb = GetComponent<Rigidbody2D>();

        // Get the player collider
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle player walking
        WalkHandler();

        // Handle player jumping
        JumpHandler();
    }

    // Make the player walk according to user input
    void WalkHandler()
    {
        // Set y velocity to the current vertical velocity so it doesn't change
        rb.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, rb.linearVelocity.y);
    }

    // Check whether the player can jump and make it jump
    void JumpHandler()
    {
        // Jump axis (typically for the space key or "Jump" axis)
        float jAxis = Input.GetAxis("Jump");

        // Is grounded
        bool isGrounded = CheckGrounded();

        // Check if the player is pressing the jump key
        if (jAxis > 0f)
        {
            // Make sure we've not already jumped on this key press
            if (!pressedJump && isGrounded)
            {
                // We are jumping on the current key press
                pressedJump = true;

                // Jumping vector (only affects y direction)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);  // Adjusted for 2D

            }
        }
        else
        {
            // Update flag so it can jump again if we press the jump key
            pressedJump = false;
        }
    }

    // Check if the object is grounded (for 2D)
    bool CheckGrounded()
    {
        // Object size in x and y (no need for z in 2D)
        float sizeX = coll.bounds.size.x;
        float sizeY = coll.bounds.size.y;

        // Position of the 4 bottom corners of the game object
        // We add a small offset in Y to detect the ground just beneath the player
        Vector2 corner1 = new Vector2(transform.position.x + sizeX / 2, transform.position.y - sizeY / 2 - 0.01f);
        Vector2 corner2 = new Vector2(transform.position.x - sizeX / 2, transform.position.y - sizeY / 2 - 0.01f);
        Vector2 corner3 = new Vector2(transform.position.x + sizeX / 2, transform.position.y - sizeY / 2 - 0.01f);
        Vector2 corner4 = new Vector2(transform.position.x - sizeX / 2, transform.position.y - sizeY / 2 - 0.01f);

        // Send a short ray down from the corners to detect the ground (2D)
        bool grounded1 = Physics2D.Raycast(corner1, Vector2.down, 0.1f);
        bool grounded2 = Physics2D.Raycast(corner2, Vector2.down, 0.1f);
        bool grounded3 = Physics2D.Raycast(corner3, Vector2.down, 0.1f);
        bool grounded4 = Physics2D.Raycast(corner4, Vector2.down, 0.1f);

        // If any corner is grounded, the object is grounded
        return (grounded1 || grounded2 || grounded3 || grounded4);
    }
}}
