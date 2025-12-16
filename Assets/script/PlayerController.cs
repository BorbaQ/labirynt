using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    public float turnSpeed = 120f;
    public float gravity = -9.81f;

    float horizontalInput;
    float forwardInput;

    CharacterController controller;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);

        Vector3 move = transform.forward * forwardInput * speed;

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;

        controller.Move((move + velocity) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("death")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
