using UnityEngine;

[CreateAssetMenu(fileName ="NewItem", menuName = "Shop/Item")]
public class ShopItemData : ScriptableObject
{
    [Header("������ ��")]
    public string itemName; // ������ �� 
    [Header("����")]
    public int price; // ���� 
    [Header("�ִ� ���� ���� ")]
    public int quantityAvailable; // ����
    [Header("������ ����")]
    [TextArea] public string description; // ������ ����

    [Header("������ ������")]
    public Sprite itemIcon; // ������ ������
}
