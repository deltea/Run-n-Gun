using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Running")]
    public bool canRun = true;
    public float maxSpeed = 7;
    public float acceleration = 500;
    public float decceleration = 800;
    public float airDecceleration = 100;
    [System.NonSerialized] public float direction = 0;

    [Header("Jumping")]
    public bool canJump = true;
    public float jumpHeight = 600;
    [System.NonSerialized] public bool isGrounded;
    [System.NonSerialized] public bool isFalling;
    private bool isOverlappingGround;
    private bool isTouchingGround;
    private bool jumping;

    [Space]

    [Range(0, 0.4f)]
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter = 0;

    [Space]

    [Range(1, 3)]
    public float fallGravityMultiplier = 2.5f;
    private float normalGravity;

    [Space]

    public float groundCheckSize = 0.47f;
    public LayerMask groundLayer;

    Rigidbody2D playerBody;

    void Start() {
        playerBody = GetComponent<Rigidbody2D>();

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

    void OnCollisionEnter2D(Collision2D collision) {
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
    }

    void OnJump(InputValue value) {
        jumping = value.Get<float>() > 0;
    }

}
