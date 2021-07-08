using Firebase.Firestore;
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
    public UserDataServer userDataServer;
    [ContextMenu("Save UserData")]
    void Save()
    {
        userDataServer = new UserDataServer();
        userDataServer.Gold = 1;
        userDataServer.Dia = 2;
        userDataServer.InvenItems = new List<InvenItemServer>();
        userDataServer.InvenItems.Add(new InvenItemServer()
        {
            ID = 1,
            UID = 2,
            Count = 1,
        });
        userDataServer.InvenItems.Add(new InvenItemServer()
        {
            ID = 1,
            UID = 2,
            Count = 4,
        });
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["MyUserInfo"] = userDataServer;
        FirestoreData.SaveToUserCloud("UserInfo", dic);
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
[FirestoreData]
public sealed class UserDataServer
{
    [FirestoreProperty] public int Gold { get; set; }
    [FirestoreProperty] public int Dia { get; set; }
    [FirestoreProperty] public int Name { get; set; }
    [FirestoreProperty] public int ID { get; set; }
    public List<InvenItemServer> InvenItems { get; set; }
}
[FirestoreData]
public sealed class InvenItemServer
{
    [FirestoreProperty] public int UID { get; set; }
    [FirestoreProperty] public int ID { get; set; }
    [FirestoreProperty] public int Count { get; set; }
    [FirestoreProperty] public int Enchant { get; set; }
    [FirestoreProperty] public DateTime GetData { get; set; }

    public override bool Equals(object obj)
    {
        if (!(obj is InvenItemServer))
        {
            return false;
        }
        InvenItemServer other = (InvenItemServer)obj;
        return UID == other.UID;
    }
    public override int GetHashCode()
    {
        return UID;
    }
}