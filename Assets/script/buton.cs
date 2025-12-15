using UnityEngine;

public class buton : MonoBehaviour
{
    void Start()
    {
        
    }

    public GameObject targetObject;



    private void OnTriggerEnter(Collider other)
    {
        targetObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        targetObject.SetActive(false);
    }
}
