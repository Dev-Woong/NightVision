using UnityEngine;

public class ShopInitializer : MonoBehaviour
{
    public ShopItemDatabase itemDatabase;

    void Start()
    {
        if (ShopManager.Instance.shopData.currentItems.Count == 0)
        {
            Debug.Log("Home �� ���� - ���� ������ �ڵ� ����");
            ShopManager.Instance.NewShopItems(itemDatabase, 4);
        }
    }
}
