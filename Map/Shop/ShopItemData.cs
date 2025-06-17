using UnityEngine;

[CreateAssetMenu(fileName ="NewItem", menuName = "Shop/Item")]
public class ShopItemData : ScriptableObject
{
    [Header("아이템 명")]
    public string itemName; // 아이템 명 

    [Header("가격")]
    public int price; // 랜덤 값 지정함

    public int basePrice; // 베이스가격
    
    [Header("변동가격")]
    public int minRandomPrice; // 최소가격
    public int maxRandomPrice; // 최대가격

    [Header("최대 구매 수량 ")]
    public int quantity; // 랜덤 값 지정함

    public int basequantity;

    [Header("변동 수량")]
    public int minRandomQuantity; // 최소 수량
    public int maxRandomQuantity; // 최대 수량

    [Header("아이템 설명")]
    [TextArea] public string description; // 아이템 설명

    [Header("아이템 아이콘")]
    public Sprite itemIcon; // 아이템 아이콘
}
