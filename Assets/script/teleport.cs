using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform exitSquare;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            if (controller != null)
                controller.enabled = false;

            other.transform.position = exitSquare.position;

            other.transform.Rotate(0f, 180f, 0f);

            if (controller != null)
                controller.enabled = true;
        }
    }
}
