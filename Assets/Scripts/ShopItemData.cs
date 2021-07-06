using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZG.Core.Type;

[UGS(typeof(ItemType))]
public enum ItemType
{
    Weapon,
    Armor,
    Potion,
    Etc,
}
[UGS(typeof(Grade))]
public enum Grade
{
    Normal,
    Rare,
    Unique,
    Legend
}
[UGS(typeof(MoneyType))]
public enum MoneyType
{
    Gold,
    Dia
}
[System.Serializable]
public class ShopItemInfo
{
    public int itemID;
    public string   name;
    //public Sprite   icon;
    public string iconName;
    public ItemType type;
    public int      sellPrice;
    public int      buyPrice;
    public string   description;

    public ShopItemInfo(MyGame.Data item)
    {
        itemID = item.itemID;
        name = item.name;
        iconName = item.iconName;
        type = item.type;
        sellPrice = item.sellPrice;
        buyPrice = item.buyPrice;
        description = item.description;
    }
    public Sprite Icon
    {
        get { return Resources.Load<Sprite>(iconName); }
    }
}
public class ShopItemData : MonoBehaviour
{
    public static ShopItemData instance;
    public List<ShopItemInfo> shopItems;
    private void Awake()
    {
        instance = this;
    }
    [ContextMenu("Load Name2", false, -10000)]
    void Load()
    {
        //UnityGoogleSheet.Load<MyGame.Data>();
        MyGame.Data.Load();
        shopItems.Clear();
        foreach (var item in MyGame.Data.DataList)
        {
            shopItems.Add(new ShopItemInfo(item));
        }
    }
    [ContextMenu("Save To Google Sheet", false, -10000)]
    void SaveToGoogleSheet()
    {
        UnityGoogleSheet.Load<MyGame.Data>();
        var firstItem = shopItems[0];
        var mapItem = MyGame.Data.DataMap[firstItem.itemID];
        mapItem.description = firstItem.description;
        //write
        UnityGoogleSheet.Write(mapItem);
    }
}

