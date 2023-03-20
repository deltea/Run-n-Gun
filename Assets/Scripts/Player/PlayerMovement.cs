using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Running")]
    public bool canRun = true;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float acceleration = 500;
    [SerializeField] private float airDecceleration = 100;
    private float direction = 0;

    [Header("Jumping")]
    public bool canJump = true;
    [SerializeField] private float jumpHeight = 800;
    [HideInInspector] private bool isGrounded;
    [HideInInspector] private bool isFalling;
    private bool isOverlappingGround;
    private bool isTouchingGround;
    private bool jumping;

    [Header("Better Platforming")]

    [Range(0, 0.4f)]
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter = 0;

    [Header("Extra Gravity")]

    [Range(1, 3)]
    [SerializeField] private float fallGravityMultiplier = 2.5f;
    private float normalGravity;

    [Header("Ground Check")]

    [SerializeField] private float groundCheckSize = 0.47f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Animation")]
    [SerializeField] private Transform graphics;
    [SerializeField] private float tilt = 10;
    [SerializeField] private float tiltSmoothing = 0.2f;
    [SerializeField] private Vector2 landSquash;
    [SerializeField] private Vector2 jumpSquash;
    [SerializeField] private float scaleSmoothing = 0.1f;

    private float targetRotation;

    Vector2 originalScale;
    Rigidbody2D playerBody;

    #region Singleton
    
    static public PlayerMovement Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    void Start() {
        playerBody = GetComponent<Rigidbody2D>();
        originalScale = graphics.localScale;

        normalGravity = playerBody.gravityScale;
    }

    void FixedUpdate() {
        Checks();

        if (canRun) Run(direction);
        if (canJump)
        {
            if (isGrounded)
            {
                coyoteTimeCounter = coyoteTime;
            } else {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (jumping)
            {
                if (coyoteTimeCounter > 0 && isGrounded)
                {
                    Jump();
                    coyoteTimeCounter = 0;
                }
            }
        }

        ExtraFallGravity();
        
        graphics.rotation = Quaternion.Lerp(graphics.rotation, Quaternion.Euler(0, 0, targetRotation), tiltSmoothing);
        graphics.localScale = Vector2.Lerp(graphics.localScale, originalScale, scaleSmoothing);
    }

    private void Run(float direction) {
        float targetSpeed = direction * maxSpeed;
        float speedDiff = targetSpeed - playerBody.velocity.x;
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : isGrounded ? acceleration : airDecceleration;
        float moveForce = Mathf.Pow(Mathf.Abs(speedDiff) * accelerationRate, 0.96f) * Mathf.Sign(speedDiff);

        playerBody.AddRelativeForce(moveForce * Vector2.right * Time.fixedDeltaTime);
    }

    private void Jump() {
        playerBody.AddRelativeForce(Vector2.up * jumpHeight * Time.fixedDeltaTime, ForceMode2D.Impulse);
        graphics.localScale = originalScale + jumpSquash;
    }

    private void Checks() {
        isOverlappingGround = Physics2D.OverlapBox(transform.position, new Vector2(groundCheckSize, 0.05f), 0, groundLayer);
        isGrounded = isOverlappingGround && isTouchingGround;
        isFalling = playerBody.velocity.y < 0;
    }

    private void ExtraFallGravity() {
        if (isFalling)
        {
            playerBody.gravityScale = normalGravity * fallGravityMultiplier;
        } else
        {
            playerBody.gravityScale = normalGravity;
        }
    }

    private void Land() {
        if (!jumping)
        {
            graphics.localScale = originalScale + landSquash;
        }
    }

    public void StopMoving() {
        canJump = false;
        canRun = false;
        playerBody.velocity = Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isTouchingGround = true;
            Checks();
            if (isTouchingGround && isOverlappingGround)
            {
                Land();
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) isTouchingGround = true;
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) isTouchingGround = false;
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireCube(transform.position, new Vector2(groundCheckSize, 0.05f));
    }

    // Input system
    void OnMovement(InputValue value) {
        direction = value.Get<float>();
        targetRotation = -direction * tilt;
    }

    void OnJump(InputValue value) {
        jumping = value.Get<float>() > 0;
    }

}
