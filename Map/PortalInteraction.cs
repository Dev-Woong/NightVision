using UnityEngine;

public class PortalInteraction : MonoBehaviour
{
    public GameObject portal;

    private void Start()
    {
        portal.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            portal.SetActive(true);
        }
    }

}
