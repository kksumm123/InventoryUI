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
    public string name;
    //public Sprite   icon;
    public string iconName;
    public ItemType type;
    public int sellPrice;
    public int buyPrice;
    public string description;

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
    // Load 함수들에서 쓰이는 데이터를 불러와 상점의 아이템리스트에 값을 넣어주는 메소드
    private void InitFromGoogleData()
    {
        shopItems.Clear();
        foreach (var item in MyGame.Data.DataList)
        {
            shopItems.Add(new ShopItemInfo(item));
        }
    }
    [ContextMenu("Load ItemInfoData at Unity Json", false, -10000)]
    void Load()
    {
        //UnityGoogleSheet.Load<MyGame.Data>();
        MyGame.Data.Load();
        InitFromGoogleData();
    }
    [ContextMenu("Load ItemInfoData at Google Sheet", false, -10000)]
    void SheetLoad()
    {
        MyGame.Data.LoadFromGoogle((list, map) => {
            foreach (var data in MyGame.Data.DataList)
            {
                InitFromGoogleData();
            }
        }, true);
        foreach (var item in MyGame.Data.DataList)
        {
            Debug.Log(item);
        }
        foreach (var item in MyGame.Data.DataMap)
        {
            Debug.Log($"{item.Key} : {item.Value}");
        }
    }
    [ContextMenu("Save To Google Sheet On FirstItem", false, -10000)]
    void SaveToGoogleSheet()
    {
        UnityGoogleSheet.Load<MyGame.Data>();
        var firstItem = shopItems[0];
        var mapItem = MyGame.Data.DataMap[firstItem.itemID];
        mapItem.description = firstItem.description;
        //write
        UnityGoogleSheet.Write(mapItem);
    }
    [ContextMenu("Save To Google Sheet All", false, -10000)]
    void SaveToGoogleSheetAll()
    {
        UnityGoogleSheet.Load<MyGame.Data>();
        int count = 0;
        foreach (var item in shopItems)
        {
#if UNITY_EDITOR
            float percent = ((float)count++ / shopItems.Count);
            UnityEditor.EditorUtility.DisplayProgressBar("구글에 데이터 입력하기", (percent * 100).ToString() + "%", percent);
#endif
            MyGame.Data data = MyGame.Data.DataMap[item.itemID];
            data.itemID = item.itemID;
            data.name = item.name;
            data.iconName = item.iconName;
            data.type = item.type;
            data.sellPrice = item.sellPrice;
            data.buyPrice = item.buyPrice;
            data.description = item.description;
            UnityGoogleSheet.Write(data);
        }
    }
}

