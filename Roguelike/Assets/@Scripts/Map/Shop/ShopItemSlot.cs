using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopItemSlot : MonoBehaviour
{
    [Header("������ ��")]
    public TMP_Text nameText;
    [Header("����")]
    public TMP_Text priceText;
    [Header("�ִ� ���� ����")]
    public TMP_Text quantityText;
    [Header("������ ���� ")]
    public TMP_Text descriptionText;

    [Header("������ ������ �̹���")]
    public Image iconImage;
    
    public void SetUpSlot(ShopItemData data)
    {
        iconImage.sprite = data.itemIcon;
        nameText.text = data.itemName;
        priceText.text = $"���� : {data.price}";
        quantityText.text = $"���� : {data.quantityAvailable}";
        descriptionText.text = data.description;
    }
}
