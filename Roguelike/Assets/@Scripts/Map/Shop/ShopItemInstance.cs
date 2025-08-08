using UnityEngine;

public class ShopItemInstance : MonoBehaviour
{
    public ShopItemData baseData;
    public int price;

    public ShopItemInstance(ShopItemData data)
    {
        baseData = data;
        price = Random.Range(data.minRandomPrice, data.maxRandomPrice);
    }
}
