using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravityScale = 10f;
    public float sensitivity = 5.0f;

    [Header("References")]
    public Transform cameraTransform;
    public Animator animator;
    
    private CharacterController characterControl;
    private Vector3 velocity;
    private bool isGrounded;
    private float verticalVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterControl = GetComponent<CharacterController>();
        if (cameraTransform == null) cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
        //HandleRotation();
    }

    void HandleMovement()
    {

    }

    void HandleJump()
    {
        float moveX=0f,moveZ=0f;
        if(Input.GetKey(KeyCode.W))
        {
            moveZ=1;
        }

        if(Input.GetKey(KeyCode.S))
        {
            moveZ=-1;
        }

        if(Input.GetKey(KeyCode.A))
        {
            moveX=-1;
        }

        if(Input.GetKey(KeyCode.D))
        {
            moveX=1;
        }
        Vector3 move = (cameraTransform.forward * moveZ + cameraTransform.right * moveX).normalized;
        move.y = 0;
        characterControl.Move(move*walkSpeed*Time.deltaTime);
        animator.Play("Running");
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        transform.Rotate(Vector3.up * mouseX);
    }
}
