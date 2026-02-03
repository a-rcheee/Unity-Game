using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public float moveSpeed = 2f;

    public float jumpForce = 12f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public string stateParam = "state";

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private SpriteRenderer sr;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }


   private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (sr != null)
        {
            if (moveInput > 0.01f) {
                sr.flipX = false;
            }
            else if (moveInput < -0.01f){
                sr.flipX = true;
            }
        }

        if (groundCheck != null) 
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (anim != null)
        {
            bool running = Mathf.Abs(moveInput) > 0.01f;

            anim.SetInteger("run", running ? 0 : 1);
            anim.SetInteger("idle", running ? 1 : 0);
        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

}
