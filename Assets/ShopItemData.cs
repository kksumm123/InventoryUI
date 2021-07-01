using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    potion,
    Etc,
}
[System.Serializable]
public class ShopItemInfo
{
    [SerializeField] string name;
    [SerializeField] Sprite icon;
    [SerializeField] ItemType type; 
    [SerializeField] int sellPrice;
    [SerializeField] int butPrice;
    [SerializeField] string description;
}
public class ShopItemData : MonoBehaviour
{
    public List<ShopItemInfo> shopItems;
}

