using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float rotationSpeed = 12f;
    public float jumpForce = 8f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private Transform orientation; // Child empty for direction reference
    private Camera cam;

    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        orientation = transform.Find("Orientation"); // Or assign in Inspector

        if (orientation == null)
        {
            Debug.LogError("Create a child empty GameObject named 'Orientation'");
        }

        // Freeze rotation so physics doesn't tip the player over
        rb.freezeRotation = true;
    }

    void Update()
    {
      HandleMovement();  
      HandleGravityManipulation();
    }

    void FixedUpdate()
    {
        ApplyMovementForce();
    }

    void HandleMovement()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck ? groundCheck.position : transform.position, groundDistance, groundMask);

        // Get input
        float horizontal = 0f;
        float vertical = 0f;

        if(Input.GetKey(KeyCode.W))
        {
            vertical=1f;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            vertical=-1f;
        }
        else
        {
            vertical = 0f;
        }

        if(Input.GetKey(KeyCode.A))
        {
            horizontal=-1f;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            horizontal=1f;
        }
        else
        {
            horizontal = 0f;
        }

        // Calculate movement relative to camera
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * vertical + right * horizontal).normalized;

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Rotate player to face movement direction
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void ApplyMovementForce()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 targetVelocity = moveDirection * moveSpeed;
            targetVelocity.y = rb.linearVelocity.y; // Preserve vertical velocity (gravity/jump)

            // Smooth velocity change
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, 10f * Time.fixedDeltaTime);
        }
        else
        {
            // Stop horizontal movement smoothly when no input
            Vector3 currentVel = rb.linearVelocity;
            currentVel.x = Mathf.Lerp(currentVel.x, 0, 8f * Time.fixedDeltaTime);
            currentVel.z = Mathf.Lerp(currentVel.z, 0, 8f * Time.fixedDeltaTime);
            rb.linearVelocity = currentVel;
        }
    }

    void HandleGravityManipulation()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Physics.gravity = new Vector3(0f,9.8f,0f);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics.gravity = new Vector3(0f,-9.8f,0f);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Physics.gravity = new Vector3(0f,9.8f,0f);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Physics.gravity = new Vector3(0f,9.8f,0f);
        }
    }

}
