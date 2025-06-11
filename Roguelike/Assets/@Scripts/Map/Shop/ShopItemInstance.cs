using UnityEngine;

public class ShopItemInstance : MonoBehaviour
{
    public ShopItemData baseData;
    public int price;
    public int quantity;

    public ShopItemInstance(ShopItemData data)
    {
        baseData = data;
        price = Random.Range(data.minRandomPrice, data.maxRandomPrice);
        quantity = Random.Range(data.minRandomQuantity, data.maxRandomQuantity);
    }
}
