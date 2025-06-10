using UnityEngine;

[CreateAssetMenu(fileName ="NewItem", menuName = "Shop/Item")]
public class ShopItemData : ScriptableObject
{
    [Header("������ ��")]
    public string itemName; // ������ �� 

    [Header("����")]
    public int basePrice; // ���̽�����

    public int price;

    [Header("��������")]
    public int minRandomPrice; // �ּҰ��ݷ���
    public int maxRandomPrice; // �ִ밡�ݷ���

    [Header("�ִ� ���� ���� ")]
    public int quantityAvailable; // ����
    [Header("������ ����")]
    [TextArea] public string description; // ������ ����

    [Header("������ ������")]
    public Sprite itemIcon; // ������ ������
}
