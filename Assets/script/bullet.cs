using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public float waitTime = 5.0f;

    public GameObject hitEffect; // particle effect prefab

    void Update()
    {
        waitTime -= Time.deltaTime;

        if (waitTime < 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}