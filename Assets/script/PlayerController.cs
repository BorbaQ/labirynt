using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public float speed = 20.0f;
    public float turnSpeed = 30.0f;
        public float horizontalInput;
    public float forwardInput;
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward* Time.deltaTime *speed * forwardInput);
        transform.Rotate(Vector3.up, turnSpeed*horizontalInput*Time.deltaTime);
    }
}
