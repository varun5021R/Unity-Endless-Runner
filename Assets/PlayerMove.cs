using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Lane Settings")]
    public float laneDistance = 3f;   // Distance between lanes
    private int currentLane = 1;      // 0 = Left, 1 = Middle, 2 = Right

    [Header("Movement")]
    public float forwardSpeed = 8f;
    public float laneChangeSpeed = 10f;

    [Header("Jump")]
    public float jumpForce = 7f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Start exactly in middle lane
        Vector3 pos = transform.position;
        pos.x = 0f;
        transform.position = pos;
    }

    void Update()
    {
        HandleLaneInput();
        HandleJumpInput();
    }

    void FixedUpdate()
    {
        MoveForward();
        MoveToLane();
        CheckGround();
    }

    // =========================
    // LANE INPUT (Keyboard)
    // =========================
    void HandleLaneInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveRight();
    }

    public void MoveLeft()
    {
        currentLane--;
        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }

    public void MoveRight()
    {
        currentLane++;
        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }

    // =========================
    // FORWARD MOVEMENT
    // =========================
    void MoveForward()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, forwardSpeed);
    }

    // =========================
    // LANE MOVEMENT
    // =========================
    void MoveToLane()
{
    float targetX = (currentLane - 1) * laneDistance;

    Vector3 pos = rb.position;
    pos.x = Mathf.Lerp(pos.x, targetX, laneChangeSpeed * Time.fixedDeltaTime);

    rb.MovePosition(pos);
}


    // =========================
    // JUMP INPUT
    // =========================
    void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
{
    Debug.Log("Jump pressed. Grounded: " + isGrounded);

    if (isGrounded)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}

    // =========================
    // GROUND CHECK
    // =========================
    void CheckGround()
{
    float rayLength = 1.2f;

    Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);

    isGrounded = Physics.Raycast(ray, rayLength, groundLayer);

    Debug.DrawRay(transform.position + Vector3.up * 0.1f,
                  Vector3.down * rayLength,
                  isGrounded ? Color.green : Color.red);
}


}
