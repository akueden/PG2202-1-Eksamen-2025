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
    private float yVelocity;    // nåværende hastighet
    private const float groundedGravity = -2f;  // holder spilleren til bakken

    // kjøres med en gang skript-instansen initialiseres
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // leser tastaturinput
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // beregner bevegelsesretning
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;
        move = transform.TransformDirection(move);

        // sjekker om karakteren står på bakken før den kan hoppe
        if (characterController.isGrounded)
        {
            yVelocity = groundedGravity;

            // sjekker om space-knappen blir klikket på
            if (Input.GetButtonDown("Jump"))
            {
                // hastighet for hvor høyt karakteren hopper
                yVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
            }
        }
        else
        {
            //  hvis spilleren er i lufta vil de falle til bakken
            yVelocity += Physics.gravity.y * Time.deltaTime;
        }

        // hastighet
        Vector3 velocity = move * moveSpeed;
        velocity.y = yVelocity;
        cc.Move(velocity * Time.deltaTime);

        if (move.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
