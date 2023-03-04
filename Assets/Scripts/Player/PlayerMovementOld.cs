using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementOld : MonoBehaviour
{

    [Header("Running")]
    public bool canRun = true;
    public int direction = 1;
    public float maxSpeed = 7;
    public float acceleration = 500;
    public float decceleration = 800;
    public float airDecceleration = 100;

    [Header("Jumping")]
    public bool canJump = true;
    public float jumpHeight = 600;
    public bool isGrounded;
    public bool isFalling;

    [Space]

    [Range(0, 0.4f)]
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter = 0;

    [Space]

    [Range(1, 3)]
    public float fallGravityMultiplier = 2.5f;
    private float normalGravity;

    [Space]

    public float groundCheckDistance = 0.47f;
    public LayerMask groundLayer;

    Rigidbody2D playerBody;

    void Start() {
        playerBody = GetComponent<Rigidbody2D>();

        normalGravity = playerBody.gravityScale;
    }

    void FixedUpdate() {
        Checks();

        if (canRun)
        {
            RunControls();
        }
        if (canJump)
        {
            JumpControls();
        }

        ExtraFallGravity();
    }

    private void RunControls() {
        Run(Input.GetAxisRaw("Horizontal"));

        if (Input.GetAxisRaw("Horizontal") > 0) direction = 1;
        if (Input.GetAxisRaw("Horizontal") < 0) direction = -1;
    }

    private void JumpControls() {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        } else {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButton("Jump") && coyoteTimeCounter > 0)
        {
            Jump();
            coyoteTimeCounter = 0;
        }
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
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
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

    private void OnDrawGizmosSelected() {
        Gizmos.DrawRay(transform.position, Vector2.down * groundCheckDistance);
    }

}
