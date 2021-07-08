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
    int dia;
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
[System.Serializable]
[FirestoreData]
public sealed class UserDataServer
{
    [SerializeField] int gold;
    [SerializeField] int dia;
    [SerializeField] string name;
    [SerializeField] int iD;
    [SerializeField] List<InvenItemServer> invenItems;

    [FirestoreProperty] public int Gold { get => gold; set => gold = value; }
    [FirestoreProperty] public int Dia { get => dia; set => dia = value; }
    [FirestoreProperty] public string Name { get => name; set => name = value; }
    [FirestoreProperty] public int ID { get => iD; set => iD = value; }
    [FirestoreProperty] public List<InvenItemServer> InvenItems { get => invenItems; set => invenItems = value; }
}
[FirestoreData]
public sealed class InvenItemServer
{
    [SerializeField] int uID;
    [SerializeField] int iD;
    [SerializeField] int count;
    [SerializeField] int enchant;
    [SerializeField] DateTime getData;

    [FirestoreProperty] public int UID { get => uID; set => uID = value; }
    [FirestoreProperty] public int ID { get => iD; set => iD = value; }
    [FirestoreProperty] public int Count { get => count; set => count = value; }
    [FirestoreProperty] public int Enchant { get => enchant; set => enchant = value; }
    [FirestoreProperty] public DateTime GetData { get => getData; set => getData = value; }

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