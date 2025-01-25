using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;  // Horizontal movement speed
    public float jumpForce = 15f; // Jump force for normal ground jumps

    [Header("Wall Jump Settings")]
    public float wallSlideSpeed = 2f; // Speed of sliding down walls
    public Vector2 wallJumpForce = new Vector2(15f, 10f); // Horizontal and vertical force for wall jumps
    public float wallReClingDelay = 0.2f; // Delay before re-clinging to the same wall
    private bool isTouchingWall; // Whether the player is touching a wall
    private bool isWallSliding; // Whether the player is sliding down a wall
    private bool canClingToWall = true; // Whether the player can cling to walls
    private float wallClingTimer; // Timer to track re-cling delay

    [Header("Ground Check")]
    public Transform groundCheck; // Position of the ground check object
    public float groundCheckRadius = 0.2f; // Radius for ground detection
    public LayerMask groundLayer; // Layer mask for ground objects
    private bool isGrounded; // Whether the player is grounded

    private Rigidbody2D rb; // Rigidbody2D component
    private float moveInput; // Horizontal input for movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        HandleWallSlide();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    // ------------------------------
    // Input Handling
    // ------------------------------
    private void HandleInput()
    {
        moveInput = Input.GetAxis("Horizontal");
    }

    // ------------------------------
    // Wall Mechanics
    // ------------------------------
    public void HandleWallSlide()
    {
        // Wall cling delay logic
        if (wallClingTimer > 0)
        {
            wallClingTimer -= Time.deltaTime;
            canClingToWall = false;
        }
        else
        {
            canClingToWall = true;
        }

        // Start wall sliding if touching a wall, not grounded, and moving toward the wall
        if (isTouchingWall && !isGrounded && moveInput == Mathf.Sign(transform.localScale.x) && canClingToWall)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed); // Control slide speed
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void HandleJump()
    {
        // Ground check logic
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle jump input
        if (Input.GetButtonDown("Jump"))
        {
            if (isWallSliding)
            {
                // Wall Jump Logic
                wallClingTimer = wallReClingDelay; // Start delay timer to prevent immediate re-cling

                // Apply jump force away from the wall
                Vector2 jumpDirection = new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpForce.x, wallJumpForce.y);
                rb.velocity = jumpDirection;

                // Flip character to face away from the wall
                FlipCharacter();
            }
            else if (isGrounded)
            {
                // Ground Jump Logic
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    private void HandleMovement()
    {
        // Allow horizontal movement if not wall sliding
        if (!isWallSliding)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        // Flip the character sprite based on movement direction
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // ------------------------------
    // Collision Detection
    // ------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }

    // ------------------------------
    // Utility Methods
    // ------------------------------
    private void FlipCharacter()
    {
        // Flip the character's sprite to face the opposite direction
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the ground check in the Unity Editor
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
