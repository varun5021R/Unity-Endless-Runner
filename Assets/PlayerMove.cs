using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Speed")]
    public float forwardSpeed = 12f;
    public float laneSpeed = 10f;

    [Header("Lane")]
    public float laneDistance = 3f;
    private int currentLane = 1; // 0 left, 1 middle, 2 right

    [Header("Jump")]
    public float jumpForce = 7f;

    private Rigidbody rb;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveForward();
        HandleLaneMovement();
    }

    void Update()
    {
        HandleKeyboardInput();
    }

    void MoveForward()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, forwardSpeed);
    }

    void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            currentLane--;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            currentLane++;

        currentLane = Mathf.Clamp(currentLane, 0, 2);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    void HandleLaneMovement()
    {
        float targetX = (currentLane - 1) * laneDistance;
        float difference = targetX - transform.position.x;
        float moveX = difference * laneSpeed;

        rb.linearVelocity = new Vector3(moveX, rb.linearVelocity.y, rb.linearVelocity.z);
    }

    public void Jump()
    {
        if (!isGrounded) return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;

        if (collision.gameObject.CompareTag("Obstacle"))
            GameManager.Instance.GameOver();
    }
}
