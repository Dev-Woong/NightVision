using UnityEngine;

[CreateAssetMenu(fileName ="NewItem", menuName = "Shop/Item")]
public class ShopItemData : ScriptableObject
{
    [Header("������ ��")]
    public string itemName; // ������ �� 

    [Header("����")]
    public int price; // ���� �� ������

    public int basePrice; // ���̽�����
    
    [Header("��������")]
    public int minRandomPrice; // �ּҰ���
    public int maxRandomPrice; // �ִ밡��

    [Header("�ִ� ���� ���� ")]
    public int quantity; // ���� �� ������

    public int basequantity;

    [Header("���� ����")]
    public int minRandomQuantity; // �ּ� ����
    public int maxRandomQuantity; // �ִ� ����

    [Header("������ ����")]
    [TextArea] public string description; // ������ ����

    [Header("������ ������")]
    public Sprite itemIcon; // ������ ������
}
