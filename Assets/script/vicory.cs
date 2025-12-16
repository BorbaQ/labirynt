using UnityEngine;

public class vicory : MonoBehaviour
{

    public GameObject player;
    public GameObject winUi;

    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerAnimator.SetBool("windance", true);
            winUi.SetActive(true);
        }
    }
}
