using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIManager2 : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;

    private List<GameObject> currentSlots = new();

    public void OpenShop()
    {
        if(ShopManager.Instance.shopData == null ||
           ShopManager.Instance.shopData.currentItems == null || 
           ShopManager.Instance.shopData.currentItems.Count == 0)
        {
            Debug.Log("상점 데이터가 없습니다.");
        }
        ClearShop();

        foreach (var item in ShopManager.Instance.shopData.currentItems)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            var slotUI = slot.GetComponent<ShopItemSlot>();
            slotUI.SetUpSlot(item);
            currentSlots.Add(slot);
        }
    }

    public void ClearShop()
    {
        foreach (var slot in currentSlots) Destroy(slot);
        currentSlots.Clear();
    }
}

