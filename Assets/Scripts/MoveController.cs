using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float xInput;


    [Header("Collision Check")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    private bool isGrounded;

    private bool isJumping;

    private bool facingRight = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimationControllers();
        CollisionChecks();
        FlipController();
        InputController();
        Movement();
        Jump();

        // xInput = Input.GetAxisRaw("Horizontal");
        // if (Input.GetKeyDown(KeyCode.Space))
        //     Jump();
    }

    private void AnimationControllers()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void InputController()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        isJumping = Input.GetKeyDown(KeyCode.Space);
    }

    private void Jump()
    {
        if (isJumping && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void FlipController()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < transform.position.x && facingRight)
            Flip();
        else if (mousePosition.x > transform.position.x && !facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
