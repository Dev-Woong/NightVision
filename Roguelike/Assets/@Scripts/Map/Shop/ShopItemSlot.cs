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
    [Header("���� ����")]
    public TMP_Text quantityText;
    [Header("������ ���� ")]
    public TMP_Text descriptionText;
    [Header("����")]
    public TMP_Text priceText;
    public Button selectedButton;
    private ShopItemInstance itemInstance;
    public void SetUpSlot(ShopItemInstance instance)
    {
        itemInstance = instance;

        iconImage.sprite = instance.baseData.itemIcon;
        nameText.text = instance.baseData.itemName;
        quantityText.text = $"���� ����  :  {instance.quantity}";
        descriptionText.text = instance.baseData.description;
        priceText.text = $"{instance.price}";
    }
    
}
