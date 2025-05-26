using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public GameObject nearObject;

    void Start()
    {
        nearObject = null;            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            nearObject = collision.gameObject;
            SceneManager.LoadScene("Scene1");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            nearObject = null;

        }
    }
}
