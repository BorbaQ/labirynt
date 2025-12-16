using UnityEngine;

public class buton2 : MonoBehaviour
{
    void Start()
    {

    }

    public GameObject targetObject;



    private void OnTriggerEnter(Collider other)
    {
        targetObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        targetObject.SetActive(true);
    }
}
