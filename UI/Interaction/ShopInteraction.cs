using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public ShopUIManager2 shopUIManager2;

    public GameObject[] gameObjects;

    private bool isPlayerinRange = false;
    private bool isShopOpen = false;

    void Start()
    {
        foreach (GameObject Gb in gameObjects)
        {
            Gb.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && isPlayerinRange == true)
        { 
            Debug.Log("InShop");
            isShopOpen = true;
            shopUIManager2.OpenShop();
            foreach (GameObject Gb in gameObjects)
            {
                Gb.SetActive(true);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //isPlayerinRange = false;
            isShopOpen = false;
            foreach (GameObject Gb in gameObjects)
            {
                Gb.SetActive(false);
                Debug.Log("OutShop");
            }
            shopUIManager2.ClearShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player")))
        {
            Debug.Log("InShop");
            isPlayerinRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerinRange = false;
            isShopOpen = false;
            foreach (GameObject Gb in gameObjects)
            {
                Gb.SetActive(false);
                Debug.Log("OutShop");
            }
            shopUIManager2.ClearShop();
        }
    }
}

