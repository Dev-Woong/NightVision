using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(fileName ="NewItem", menuName = "Shop/Item")]
public class ShopItemData : ScriptableObject
{
    [Header("������ ��")]
    public string itemName; // ������ ��     
    [Header("��������")]
    public int minRandomPrice; // �ּҰ���
    public int maxRandomPrice; // �ִ밡��

    [Header("���� ����")]
    public int minRandomQuantity; // �ּ� ����
    public int maxRandomQuantity; // �ִ� ����

    [Header("������ ����")]
    [TextArea] public string description; // ������ ����

    [Header("������ ������")]
    public Sprite itemIcon; // ������ ������
    [Header("������ �������ͽ�")]
    public float itemAtk;
    public float itemSpeed;
    public int itemDef;
    public int itemMaxHp;
    public int itemEnergy;
    public float itemEnergyRecovery;
}
