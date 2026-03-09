using UnityEngine;

public class bullet : MonoBehaviour
{

    public float speed;
    public float waitTime = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waitTime-=1*Time.deltaTime;
        if (waitTime < 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
