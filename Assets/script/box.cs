using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject targetObject;

    private void Start() { targetObject.SetActive(false); }

    private void OnTriggerEnter(Collider other)
    {
        targetObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        targetObject.SetActive(false);
    }
}


