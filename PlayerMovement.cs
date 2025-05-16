using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 2.0f;

    [Header("Jump Settings")]
    public float jumpForce = 6.0f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleRotation();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
        CheckGround();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * moveZ + transform.right * moveX;
        rb.velocity = new Vector3(moveDirection.x * moveSpeed,
                                  rb.velocity.y,
                                  moveDirection.z * moveSpeed);
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(0, mouseX, 0);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position,
                                     Vector3.down,
                                     groundCheckDistance + 0.1f,
                                     groundLayer);
    }
}
