using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float playerSpeed = 10f;

    [Header("Jump Settings")]
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float jumpTime;
    [SerializeField] float fallMultiplier = 3f;
    [SerializeField] float jumpMultiplier;
    bool isJumping;

    Vector2 vecGravity;
    float jumpCounter;

    private Rigidbody2D rb;
    float horizontal;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 2.4f;

    private bool facingRight = true;

    [Header("Camera")]
    public Camera _camera;
    public float cameraSmoothSpeed = 5f;
    private float targetCamY;

    void Start()
    {
        vecGravity = new Vector2 (0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 300;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        Move();
        Jump();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(horizontal * playerSpeed, rb.linearVelocity.y);
        if (horizontal > 0 && !facingRight) Flip();
        else if (horizontal < 0 && facingRight) Flip();
        targetCamY = transform.position.y;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            isJumping = true;
            jumpCounter = 0;
        }

        if (rb.linearVelocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            rb.linearVelocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }

        if (rb.linearVelocity.y < 0 && isJumping)
        {
            rb.linearVelocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }

    private void LateUpdate()
    {
        SmoothCamera();
    }

    void SmoothCamera()
    {
        Vector3 camPos = _camera.transform.position;
        camPos.y = Mathf.Lerp(camPos.y, targetCamY, cameraSmoothSpeed * Time.deltaTime);
        _camera.transform.position = camPos;
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
