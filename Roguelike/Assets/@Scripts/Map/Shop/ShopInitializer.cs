using UnityEngine;

public class ShopInitializer : MonoBehaviour
{
    public ShopItemDatabase itemDatabase;

    void Start()
    {
        if (ShopManager.Instance.shopData.currentItems.Count == 0)
        {
            Debug.Log("Home 씬 진입 - 상점 아이템 자동 생성");
            ShopManager.Instance.NewShopItems(itemDatabase, 4);
        }
    }
}
