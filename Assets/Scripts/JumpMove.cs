using UnityEngine;

public class JumpMove : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Jump System")]
    [SerializeField] float jumpTime;
    [SerializeField] int jumpPower = 10;
    [SerializeField] float fallMultiplier = 3f;
    [SerializeField] float jumpMultiplier;

    public Transform groundCheck;
    public LayerMask ground;
    Vector2 vecGravity;

    bool isJumping;
    float jumpCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vecGravity = new Vector2 (0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void Jump()
    {

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);
            isJumping = true;
            jumpCounter = 0;
        }

        if (rb.linearVelocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            rb.linearVelocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(0.6f, 0.06f), 0, ground);
    }
}
