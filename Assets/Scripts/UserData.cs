using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InvenItemInfo
{
    public int itemID;
    public int count;
    public string getDate; //획득한 날짜

    public ShopItemInfo GetShopItemInfo()
    {
        return ShopItemData.instance.shopItems.Find(x => x.itemID == itemID);
    }
}

public class UserData : MonoBehaviour
{
    public static UserData instance;

    public List<InvenItemInfo> invenItems;

    int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            // 구글에 변경된 골드 저장
            SaveGoldToCloud();
            MoneyUI.instance?.RefreshUI();
        }
    }
    public int dia;
    public int Dia
    {
        get { return dia; }
        set
        {
            dia = value;
            MoneyUI.instance?.RefreshUI();
        }
    }
    private void Awake()
    {
        instance = this;
    }

    public static void SetGold(int gold)
    {
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.Save();
        if (instance)
        {
            instance.Gold = gold;
            MoneyUI.instance.RefreshUI();
        }
    }
}