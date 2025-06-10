using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    public ShopItemDatabase itemDatabase;
    public GameObject slotPrefab;
    public Transform slotParent;

    private List<GameObject> currentSlots = new List<GameObject>();

    public void OpenShop()
    {
        ClearShop();

        List<ShopItemData> RandomItems = new List<ShopItemData>(itemDatabase.items);
        
        for(int i = 0; i < RandomItems.Count; i++)
        {
            int randIndex = Random.Range(i, RandomItems.Count);
            var temp = RandomItems[i];
            RandomItems[i] = RandomItems[randIndex];
            RandomItems[randIndex] = temp;
        }

        foreach (var itemData in RandomItems)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotParent);
            ShopItemSlot slot = slotObj.GetComponent<ShopItemSlot>();

            // 아이템 복사본 만들기
            ShopItemData runtimeItem = ScriptableObject.CreateInstance<ShopItemData>();
            runtimeItem.itemName = itemData.itemName;
            runtimeItem.basePrice = itemData.basePrice;
            runtimeItem.price = Random.Range(itemData.minRandomPrice, itemData.maxRandomPrice + 1);
            runtimeItem.quantityAvailable = itemData.quantityAvailable;
            runtimeItem.description = itemData.description;
            runtimeItem.itemIcon = itemData.itemIcon;

            slot.SetUpSlot(runtimeItem);

            
        }
        //PopulateShop();
    }

    public void CloseShop()
    {
        ClearShop();
    }

    void ClearShop()
    {
        foreach (var slot in currentSlots)
        {
            Destroy(slot);
        }
        currentSlots.Clear();
    }

    
    //void PopulateShop()
    //{
    //    foreach (var item in itemDatabase.items)
    //    {
    //        GameObject slotObj = Instantiate(slotPrefab, slotParent);
    //        ShopItemSlot slot = slotObj.GetComponent<ShopItemSlot>();
    //        slot.SetUpSlot(item);
    //    }
    //}



}
