using UnityEngine;

public class GuardController : MonoBehaviour
{
    public GameObject Guard;

    private void Start()
    {
        Guard.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Guard != null)
                Guard.SetActive(true);
            else return;
        }
    }
}
