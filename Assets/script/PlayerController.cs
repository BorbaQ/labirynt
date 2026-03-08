using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 6f;
    public float jumpHeight = 1.6f;
    public float gravity = -9.81f;

    [Header("Mouse")]
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    public bool cameraLock;

    float forwardInput;
    Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!cameraLock)
        {
            Look();
        }
        Move();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float forward = Input.GetKey(KeyCode.W) ? 1 : 0;
        float backward = Input.GetKey(KeyCode.S) ? 1 : 0;
        float right = Input.GetKey(KeyCode.D) ? 1 : 0;
        float left = Input.GetKey(KeyCode.A) ? 1 : 0;

        forwardInput = Input.GetAxis("Vertical");
        animator.SetFloat("Forward", forwardInput);

        Vector3 move = transform.right * (right - left) + transform.forward * (forward - backward);
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}