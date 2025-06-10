using UnityEngine;

[CreateAssetMenu(fileName ="NewItem", menuName = "Shop/Item")]
public class ShopItemData : ScriptableObject
{
    [Header("아이템 명")]
    public string itemName; // 아이템 명 

    [Header("가격")]
    public int basePrice; // 베이스가격

    public int price;

    [Header("변동가격")]
    public int minRandomPrice; // 최소가격랜덤
    public int maxRandomPrice; // 최대가격랜덤

    [Header("최대 구매 수량 ")]
    public int quantityAvailable; // 수량
    [Header("아이템 설명")]
    [TextArea] public string description; // 아이템 설명

    [Header("아이템 아이콘")]
    public Sprite itemIcon; // 아이템 아이콘
}
