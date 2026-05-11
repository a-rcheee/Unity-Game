using UnityEngine;
       
public class NewMonoBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 10f;

    public float jumpForce = 12f;
    public float wallJumpForceX = 5f;
    public float wallJumpForceY = 12f;
    public float wallJumpLockTime = 0.1f;
    private float wallJumpTimer = 0.1f;
    public float wallJumpCooldown = 0.1f;
    private float wallJumpCooldownTimer = 0f;
    private int wallSide = 0;
    public float groundAcceleration = 20f;
    public float groundDeceleration = 30f;
    public float airAcceleration = 12f;
    public float airDeceleration = 10f;

    public float coyoteTime = 0.1f;
    private float coyoteTimer = 0f;

    public float jumpBufferTime = 0.1f;
    private float jumpBufferTimer = 0f;

    public float fallGravityMultiplier = 2f;
    public float lowJumpGravityMultiplier = 2.5f;

    public float dashSpeed = 6f;
    public float dashTime = 0.12f;
    public float dashCooldown = 0.25f;

    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private bool isDashing = false;
    private bool canDash = true;
    private int dashDirection = 1;
    private int facingDirection = 1;

    public LayerMask groundLayer;
    public Transform groundCheck;

    public LayerMask wallLayer;
    public Transform wallCheckLeft;
    public Transform wallCheckRight;

    public Vector2 wallCheckSize = new Vector2(0.01f, 0.3f);

    public Vector3 respawnPoint;

    public float wallSlideSpeed = 2.0f;
    private float defaultGravityScale;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private bool isTouchingLeftWall;
    private bool isTouchingRightWall;
    private bool canWallSlide = false;
    private bool canDashAbility = false;
    private bool isWallSliding = false;
    private bool isWallHolding = false;
    private bool wallJumpUsed = false;
    private SpriteRenderer sr;
    private Animator anim;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        defaultGravityScale = rb.gravityScale;
    }

    private void Start()
    {
        respawnPoint = transform.position;
    }


    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (wallJumpCooldownTimer > 0f)
        {
            wallJumpCooldownTimer -= Time.deltaTime;
        }

        if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (sr != null)
        {
            if (moveInput > 0.01f)
            {
                sr.flipX = false;
                facingDirection = 1;
            }
            else if (moveInput < -0.01f)
            {
                sr.flipX = true;
                facingDirection = -1;
            }
        }

        if (groundCheck != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.08f, groundLayer);
            isGrounded = hit.collider != null;
        }

        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBufferTime;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        if (wallCheckLeft != null)
        {
            isTouchingLeftWall = Physics2D.OverlapBox(wallCheckLeft.position, wallCheckSize, 0f, wallLayer);
        }

        if (wallCheckRight != null)
        {
            isTouchingRightWall = Physics2D.OverlapBox(wallCheckRight.position, wallCheckSize, 0f, wallLayer);
        }

        if (wallJumpUsed && wallJumpTimer <= 0f && (isTouchingLeftWall || isTouchingRightWall))
        {
            wallJumpUsed = false;
        }

        if (isGrounded || isWallSliding || isWallHolding)
        {
            canDash = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDashAbility && canDash && dashCooldownTimer <= 0f)
        {
            StartDash();
        }

        isWallSliding = false;
        isWallHolding = false;
        wallSide = 0;

        bool onLeftWall = canWallSlide && isTouchingLeftWall && !isGrounded;
        bool onRightWall = canWallSlide && isTouchingRightWall && !isGrounded;

        if ((onLeftWall || onRightWall) && wallJumpTimer <= 0f)
        {
            if (onLeftWall)
            {
                wallSide = -1;
            }
            else if (onRightWall)
            {
                wallSide = 1;
            }

            if (Input.GetKey(KeyCode.F))
            {
                isWallHolding = true;
            }
            else if (rb.linearVelocity.y < 0f)
            {
                isWallSliding = true;
            }
        }

        if (jumpBufferTimer > 0f)
        {
            if (coyoteTimer > 0f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                jumpBufferTimer = 0f;
                coyoteTimer = 0f;
            }
            else if ((isWallSliding || isWallHolding) && wallJumpCooldownTimer <= 0f && !wallJumpUsed)
            {
                rb.gravityScale = defaultGravityScale;
                wallJumpTimer = wallJumpLockTime;
                wallJumpCooldownTimer = wallJumpCooldown;
                wallJumpUsed = true;

                if (wallSide == -1)
                {
                    rb.linearVelocity = new Vector2(wallJumpForceX, wallJumpForceY);
                }
                else if (wallSide == 1)
                {
                    rb.linearVelocity = new Vector2(-wallJumpForceX, wallJumpForceY);
                }

                jumpBufferTimer = 0f;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if (anim != null)
        {
            bool running = Mathf.Abs(moveInput) > 0.01f && isGrounded;
            bool jumping = !isGrounded && !isWallSliding && !isWallHolding && !isDashing;

            anim.SetInteger("run", running ? 1 : 0);
            anim.SetBool("wall", isWallSliding);
            anim.SetBool("wallHold", isWallHolding);
            anim.SetBool("jump", jumping);
            anim.SetBool("dash", isDashing);
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;

            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);

            if (dashTimer <= 0f)
            {
                isDashing = false;
                rb.gravityScale = defaultGravityScale;
            }

            return;
        }

        if (wallJumpTimer <= 0f)
        {
            float targetSpeed = moveInput * moveSpeed;
            float speedDiff = targetSpeed - rb.linearVelocity.x;

            float accelRate;

            if (isGrounded)
            {
                accelRate = Mathf.Abs(targetSpeed) > 0.01f ? groundAcceleration : groundDeceleration;
            }
            else
            {
                accelRate = Mathf.Abs(targetSpeed) > 0.01f ? airAcceleration : airDeceleration;
            }

            float movement = speedDiff * accelRate;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x + movement * Time.fixedDeltaTime, rb.linearVelocity.y);
        }

        if (!isWallHolding)
        {
            if (rb.linearVelocity.y < 0f)
            {
                rb.gravityScale = defaultGravityScale * fallGravityMultiplier;
            }
            else if (rb.linearVelocity.y > 0f && !Input.GetButton("Jump"))
            {
                rb.gravityScale = defaultGravityScale * lowJumpGravityMultiplier;
            }
            else
            {
                rb.gravityScale = defaultGravityScale;
            }
        }

        if (isWallHolding)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }
        else
        {
            if (rb.linearVelocity.y < 0f)
            {
                rb.gravityScale = defaultGravityScale * fallGravityMultiplier;
            }
            else if (rb.linearVelocity.y > 0f && !Input.GetButton("Jump"))
            {
                rb.gravityScale = defaultGravityScale * lowJumpGravityMultiplier;
            }
            else
            {
                rb.gravityScale = defaultGravityScale;
            }

            if (isWallSliding && rb.linearVelocity.y < -wallSlideSpeed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
            }
        }
    }

    public void SetCheckpoint(Vector3 newPoint)
    {
        respawnPoint = newPoint;
    }

    public void Respawn()
    {
        transform.position = respawnPoint;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        wallJumpTimer = 0f;
        wallJumpCooldownTimer = 0f;
        isWallSliding = false;
        isWallHolding = false;
        wallJumpUsed = false;
        isDashing = false;
        canDash = true;
        dashTimer = 0f;
        dashCooldownTimer = 0f;
    }

    private void StartDash()
    {
        dashDirection = facingDirection;

        if (moveInput > 0.01f)
        {
            dashDirection = 1;
        }
        else if (moveInput < -0.01f)
        {
            dashDirection = -1;
        }

        isDashing = true;
        canDash = false;
        dashTimer = dashTime;
        dashCooldownTimer = dashCooldown;

        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);
    }

    public bool IsWallHolding()
    {
        return isWallHolding;
    }

    public void UnlockWallSlide()
    {
        canWallSlide = true;
    }

    public void UnlockDash()
    {
        canDashAbility = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (wallCheckLeft != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(wallCheckLeft.position, wallCheckSize);
        }

        if (wallCheckRight != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(wallCheckRight.position, wallCheckSize);
        }

        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * 0.08f);
        }
    }
}