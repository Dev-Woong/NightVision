using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    public ShopItemDatabase itemDatabase;
    public GameObject slotPrefab;
    public Transform slotParent;

    void Start()
    {
        PopulateShop();    
    }

    void PopulateShop()
    {
        foreach (var item in itemDatabase.items)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotParent);
            ShopItemSlot slot = slotObj.GetComponent<ShopItemSlot>();
            slot.SetUpSlot(item);
        }
    }



}
