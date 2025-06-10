using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemSlot : MonoBehaviour
{
    [Header("아이템 명")]
    public TMP_Text nameText;
    [Header("가격")]
    public TMP_Text priceText;
    [Header("최대 구매 수량")]
    public TMP_Text quantityText;
    [Header("아이템 설명 ")]
    public TMP_Text descriptionText;

    [Header("아이템 아이콘 이미지")]
    public Image iconImage;
    
    public void SetUpSlot(ShopItemData data)
    {
        iconImage.sprite = data.itemIcon;
        nameText.text = data.itemName;
        priceText.text = $"가격 : {data.price}";
        quantityText.text = $"수량 : {data.quantityAvailable}";
        descriptionText.text = data.description;
    }
}
