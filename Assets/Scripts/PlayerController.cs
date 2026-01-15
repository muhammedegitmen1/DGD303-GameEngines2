using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;
    public float jumpForce = 12f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private bool isFacingRight = true; // player's face dir
    
    public Animator animator; 
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move input
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    
        // 4. ANIMATION LOGIC
        // Set 'Speed' to absolute value (always positive). 0 = Idle, >0 = Run
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        
        // Set 'IsJumping' based on ground state. If not grounded, we are jumping/falling.
        animator.SetBool("IsJumping", !isGrounded);
        
        // Flipping logic
        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // Apply physics movement
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
    }

    // Flipping method
    void Flip()
    {
        isFacingRight = !isFacingRight;
        
        // Rotate 180 deg
        Vector3 rot = transform.eulerAngles;
        rot.y += 180;
        transform.eulerAngles = rot;
    }
}