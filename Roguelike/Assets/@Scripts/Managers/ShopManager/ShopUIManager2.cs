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
            Debug.Log("ShopItem 생성");
            GameObject slot = Instantiate(slotPrefab, slotParent);
            var slotUI = slot.GetComponent<ShopItemSlot>();
            slotUI.SetUpSlot(item);
            currentSlots.Add(slot);
        }
    }
    //public void BuyingItem() // 캐릭터 능력치 리펙토링 필요 
    //{
    //    var player = PlayerData.Instance;

    //    if (itemInstance.quantity <= 0)
    //    {
    //        Debug.Log("품절입니다."); // 품절
    //        return;
    //    }

    //    if (player.gold < itemInstance.price)
    //    {
    //        Debug.Log("골드 부족!"); // 돈 부족
    //        return;
    //    }

       
    //    player.gold -= itemInstance.price; // 잼 차감

    //    player.ApplyItemStats(itemInstance.baseData); //능력치 추가

    //    itemInstance.quantity--;
    //    quantityText.text = $"남은 수량  :  {itemInstance.quantity}"; // 수량 감소 및 UI 갱신

    //    if (itemInstance.quantity <= 0)
    //    {
    //        selectedButton.interactable = false;
    //    }

    //    Debug.Log($"{itemInstance.baseData.itemName} 아이템 구매 완료!");
    //}
    public void ClearShop()
    {
        foreach (var slot in currentSlots) Destroy(slot);
        currentSlots.Clear();
    }
}

