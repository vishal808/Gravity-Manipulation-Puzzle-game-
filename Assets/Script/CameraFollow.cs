using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 5, -10);

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensivity = 4.0f;
    private const float YMin = -50.0f;
    private const float YMax = 50.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;
        currentY = Mathf.Clamp(currentY, YMin, YMax);
        Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);
        Vector3 desiredPosition = target.position +rotation*offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
        if(Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
