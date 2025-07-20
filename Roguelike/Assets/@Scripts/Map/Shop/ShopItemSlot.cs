using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemSlot : MonoBehaviour
{
    private ShopItemData itemData;
    [Header("아이템 아이콘 이미지")]
    public Image iconImage;
    [Header("아이템 명")]
    public TMP_Text nameText;
    [Header("구매 수량")]
    public TMP_Text quantityText;
    [Header("아이템 설명 ")]
    public TMP_Text descriptionText;
    [Header("가격")]
    public TMP_Text priceText;
    public Button selectedButton;
    private ShopItemInstance itemInstance;
    public void SetUpSlot(ShopItemInstance instance)
    {
        itemInstance = instance;

        iconImage.sprite = instance.baseData.itemIcon;
        nameText.text = instance.baseData.itemName;
        quantityText.text = $"남은 수량  :  {instance.quantity}";
        descriptionText.text = instance.baseData.description;
        priceText.text = $"{instance.price}";
    }
    
}
