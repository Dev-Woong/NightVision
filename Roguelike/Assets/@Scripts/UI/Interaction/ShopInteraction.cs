using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    public ShopUIManager2 shopUIManager2;

    public GameObject[] gameObjects;

    private bool isPlayerinRange = false;
    private bool isShopOpen = false;
    public int ShopOpenCount;
    void Start()
    {
        foreach (GameObject Gb in gameObjects)
        {
            Gb.SetActive(false);
        }
    }
    public void ShopProcess()
    {
        if (Input.GetKeyDown(KeyCode.F) && isPlayerinRange == true)
        {
            if (isShopOpen == false && ShopOpenCount == 1)
            {
                ShopOpenCount = 0;
                isShopOpen = true;
                shopUIManager2.OpenShop();

                BGMManager.Instance.EnterShopBGM(isShopOpen);
            }
            else if (isShopOpen == true)
            {
                isShopOpen = false;
                shopUIManager2.OpenShop();
                shopUIManager2.ClearShop();

                BGMManager.Instance.EnterShopBGM(isShopOpen);
            }
            foreach (GameObject Gb in gameObjects)
            {
                Gb.SetActive(isShopOpen);
            }
        }
    }
    void Update()
    {
        ShopProcess();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player")))
        {
            isPlayerinRange = true;
            ShopOpenCount = collision.GetComponent<PlayerController>().shopOpenCount;
            collision.GetComponent<PlayerController>().shopOpenCount = 0;
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
            }
            shopUIManager2.ClearShop();
        }
    }
}

