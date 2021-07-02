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
        set { gold = value; }
    }
    public int dia;
    private void Awake()
    {
        instance = this;

        Load();
    }
    private void OnDestroy()
    {
        Save();
    }
    private void Load()
    {
        if (PlayerPrefs.HasKey("gold"))
        {
            Gold = PlayerPrefs.GetInt("gold");
            dia = PlayerPrefs.GetInt("dia");

            int itemCount = PlayerPrefs.GetInt("invenItems.Count", invenItems.Count);
            for (int i = 0; i < itemCount; i++)
            {
                var loadItem = new InvenItemInfo();
                loadItem.itemID = PlayerPrefs.GetInt("invenItems.itemID" + i);
                loadItem.count = PlayerPrefs.GetInt("invenItems.count" + i);
                loadItem.getDate = PlayerPrefs.GetString("invenItems.getDate" + i);
                invenItems.Add(loadItem);
            }
        }
        else
        {
            Gold = 1100;
            dia = 120;
        }
    }

    public static void SetGold(int gold)
    {
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.Save();
        if(instance)
        {
            instance.Gold = gold;
            MoneyUI.instance.RefreshUI();
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("invenItems.Count", invenItems.Count);
        for (int i = 0; i < invenItems.Count; i++)
        {
            var saveItem = invenItems[i];
            PlayerPrefs.SetInt("invenItems.itemID" + i, saveItem.itemID);
            PlayerPrefs.SetInt("invenItems.count" + i, saveItem.count);
            PlayerPrefs.SetString("invenItems.getDate" + i, saveItem.getDate);
        }
        PlayerPrefs.SetInt("gold", Gold);
        PlayerPrefs.SetInt("dia", dia);
    }
}