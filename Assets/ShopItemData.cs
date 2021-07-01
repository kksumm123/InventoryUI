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
    public string name;
    public Sprite icon;
    public ItemType type; 
    public int sellPrice;
    public int buyPrice;
    public string description;
}
public class ShopItemData : MonoBehaviour
{
    public static ShopItemData instance;
    public List<ShopItemInfo> shopItems;
    private void Awake()
    {
        instance = this;
    }
}

