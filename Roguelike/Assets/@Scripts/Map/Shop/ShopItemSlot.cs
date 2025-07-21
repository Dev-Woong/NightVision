using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemSlot : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text nameText;
    public TMP_Text quantityText;
    public TMP_Text descriptionText;
    public TMP_Text priceText;
    public float atk;
    public int maxHp;
    public float speed;
    public int def;
    public int price;
    public int quantity;
    public int energy;
    public float energyRecovery;
    public Button selectedButton;
    public void SetUpSlot(ShopItemInstance instance)
    {
        iconImage.sprite = instance.baseData.itemIcon;
        nameText.text = instance.baseData.itemName;
        quantityText.text = $"남은 수량  :  {instance.quantity}";
        descriptionText.text = instance.baseData.description;
        priceText.text = $"{instance.price}";
        def = instance.baseData.itemDef;
        atk = instance.baseData.itemAtk;
        maxHp = instance.baseData.itemMaxHp;
        speed = instance.baseData.itemSpeed;
        energy = instance.baseData.itemEnergy;
        energyRecovery = instance.baseData.itemEnergyRecovery;
        price = instance.price;
        quantity = instance.quantity;   
    }
    public void BuyingItem() 
    {
        if (quantity <= 0)
        {
            return;
        }
        if (PlayerStatus.Instance.jamStack < price)
        {
            return;
        }
        PlayerStatus.Instance.SetItemStats(this);
        quantity--;
        quantityText.text = $"남은 수량  :  {quantity}"; 

        if (quantity <= 0)
        {
            selectedButton.interactable = false;
        }

    }
}

