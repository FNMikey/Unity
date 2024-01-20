using UnityEngine;

public class PlayerBHV : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public Transform camTransform;

    Rigidbody rb;
    Vector3 movement;
    Vector3 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (camTransform == null)
        {
            camTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Calculate movement vector
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Mouse position
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            mousePos = cameraRay.GetPoint(rayLength);

            // Determine the rotation towards the mouse position
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(mousePos.x - transform.position.x, 0f, mousePos.z - transform.position.z));
            rb.MoveRotation(newRotation);
        }

        // Sprinting
        movement = new Vector3(moveDirection.x, 0f, moveDirection.z).normalized;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= sprintMultiplier;
        }
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
