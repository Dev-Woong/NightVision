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
            Debug.Log("���� �����Ͱ� �����ϴ�.");
        }
        ClearShop();

        foreach (var item in ShopManager.Instance.shopData.currentItems)
        {
            Debug.Log("ShopItem ����");
            GameObject slot = Instantiate(slotPrefab, slotParent);
            var slotUI = slot.GetComponent<ShopItemSlot>();
            slotUI.SetUpSlot(item);
            currentSlots.Add(slot);
        }
    }
    //public void BuyingItem() // ĳ���� �ɷ�ġ �����丵 �ʿ� 
    //{
    //    var player = PlayerData.Instance;

    //    if (itemInstance.quantity <= 0)
    //    {
    //        Debug.Log("ǰ���Դϴ�."); // ǰ��
    //        return;
    //    }

    //    if (player.gold < itemInstance.price)
    //    {
    //        Debug.Log("��� ����!"); // �� ����
    //        return;
    //    }

       
    //    player.gold -= itemInstance.price; // �� ����

    //    player.ApplyItemStats(itemInstance.baseData); //�ɷ�ġ �߰�

    //    itemInstance.quantity--;
    //    quantityText.text = $"���� ����  :  {itemInstance.quantity}"; // ���� ���� �� UI ����

    //    if (itemInstance.quantity <= 0)
    //    {
    //        selectedButton.interactable = false;
    //    }

    //    Debug.Log($"{itemInstance.baseData.itemName} ������ ���� �Ϸ�!");
    //}
    public void ClearShop()
    {
        foreach (var slot in currentSlots) Destroy(slot);
        currentSlots.Clear();
    }
}

