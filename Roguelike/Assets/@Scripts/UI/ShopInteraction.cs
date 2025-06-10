using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public ShopUIManager ShopUIManager;

    public GameObject[] gameObjects;

    void Start()
    {
        foreach (GameObject Gb in gameObjects)
        {
            Gb.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        { 
            Debug.Log("InShop");
            ShopUIManager.OpenShop();
            foreach (GameObject Gb in gameObjects)
            {
                Gb.SetActive(true);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ShopUIManager.CloseShop();
            foreach (GameObject Gb in gameObjects)
            {
                Gb.SetActive(false);
                Debug.Log("OutShop");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player")))
        {
            Debug.Log("InShop");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShopUIManager.CloseShop();
            foreach (GameObject Gb in gameObjects)
            {
                Gb.SetActive(false);
                Debug.Log("OutShop");
            }
        }
    }
}

