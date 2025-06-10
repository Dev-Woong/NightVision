using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemSlot : MonoBehaviour
{

    private ShopItemData itemData;

    [Header("������ ������ �̹���")]
    public Image iconImage;

    [Header("������ ��")]
    public TMP_Text nameText;
    [Header("�ִ� ���� ����")]
    public TMP_Text quantityText;
    [Header("������ ���� ")]
    public TMP_Text descriptionText;
    [Header("����")]
    public TMP_Text priceText;


    public void SetUpSlot(ShopItemData data)
    {
        itemData = data;

        iconImage.sprite = data.itemIcon;
        nameText.text = data.itemName;
        quantityText.text = $"���� ���� :  {data.quantityAvailable}";
        descriptionText.text = data.description;
        priceText.text = $"{data.price}";
    }

    public void RefreshQuantity()
    {
        quantityText.text = $"���� ���� :  {itemData.quantityAvailable}";
    }
}
