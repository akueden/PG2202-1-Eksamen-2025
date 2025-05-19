using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // bevegelse
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;

    // hopping
    public float jumpHeight = 1.6f;

    private CharacterController characterController;
    private Rigidbody rb;
    private float yVelocity;
    private const float groundedGravity = -2f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        // sånn at karakteren ikke tipper over
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        // tastaturinput
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // bevegelsesretning i world space
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;
        move = transform.TransformDirection(move);

        // hopping
        if (characterController.isGrounded)
        {
            yVelocity = groundedGravity;

            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            }
        }
        else
        {
            yVelocity += Physics.gravity.y * Time.deltaTime;
        }

        // bevegelse
        Vector3 velocity = move * moveSpeed;
        velocity.y = yVelocity;
        characterController.Move(velocity * Time.deltaTime);

        // snur figuren mot bevegelsesretningen
        if (move.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(new Vector3(move.x, 0f, move.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                          targetRot,
                                                          rotationSpeed * Time.deltaTime);
        }
    }
}
