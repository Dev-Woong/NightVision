using System.Collections.Generic;
using UnityEngine;

public class ShopSessionData
{
    public List<ShopItemInstance> currentItems = new();
}

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    
    public ShopSessionData shopData = new ShopSessionData();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void NewShopItems(ShopItemDatabase database, int itemCount)
    {
        shopData.currentItems.Clear();

        List<ShopItemData> itemDatas = new(database.items);
        Shuffle(itemDatas);

        for(int i = 0; i < /*Mathf.Min(itemCount, itemDatas.Count)*/itemCount; i++)
        {
            ShopItemInstance sInstance = new (itemDatas[i]);
            shopData.currentItems.Add(sInstance);
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for(int i = 0; i < list.Count;i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }


}
