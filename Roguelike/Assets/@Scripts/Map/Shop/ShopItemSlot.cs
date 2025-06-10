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
    [Header("최대 구매 수량")]
    public TMP_Text quantityText;
    [Header("아이템 설명 ")]
    public TMP_Text descriptionText;
    [Header("가격")]
    public TMP_Text priceText;


    public void SetUpSlot(ShopItemData data)
    {
        itemData = data;

        iconImage.sprite = data.itemIcon;
        nameText.text = data.itemName;
        quantityText.text = $"남은 수량 :  {data.quantityAvailable}";
        descriptionText.text = data.description;
        priceText.text = $"{data.price}";
    }

    public void RefreshQuantity()
    {
        quantityText.text = $"남은 수량 :  {itemData.quantityAvailable}";
    }
}
