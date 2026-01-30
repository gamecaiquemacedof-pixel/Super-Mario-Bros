using UnityEngine;

public class MarioController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 16f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.10f;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    Animator anim;
    AudioSource audSrc;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audSrc = GetComponent<AudioSource>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        Walk();
        Jump();
    }

    void Walk()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);

        if (horizontal > 0.01f) transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
    }

    void Jump()
    {
        bool grounded = IsGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            // zera o Y pra pulo consistente
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audSrc.Play();
        }

        if (anim != null) anim.SetBool("isJumping", !grounded);
    }

    bool IsGrounded()
    {
        if (groundCheck == null) return false;

        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
