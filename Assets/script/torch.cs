using UnityEngine;

public class ActivateOnBulletTrigger : MonoBehaviour
{
    public GameObject objectToActivate;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }
}