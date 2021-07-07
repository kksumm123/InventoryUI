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
    private void SaveGoldToCloud()
    {
        FirestoreData.SaveToUserCloud("UserInfo", "gold", Gold);
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

    IEnumerator Start()
    {
        //if (FirestoreManager.instance.userID != null)
        // 스트링 비어있는지 안전하게 확인하는 법
        while (string.IsNullOrEmpty(FirestoreManager.instance.userID))
            yield return null;

        Load();
    }
    private void OnDestroy()
    {
        Save();
    }
    private void Load()
    {
        FirestoreData.LoadFromUserCloud("UserInfo", LoadCallBack);
    }

    void LoadCallBack(IDictionary<string, object> obj)
    {
        if (obj.ContainsKey("Gold"))
            Gold = Convert.ToInt32(obj["Gold"]);
        else
            Gold = 1100;

        if (obj.ContainsKey("Dia"))
            Dia = Convert.ToInt32(obj["Dia"]);
        else
            Dia = 110;
    }

    public static void SetGold(int gold)
    {
        //PlayerPrefs.SetInt("gold", gold);
        //PlayerPrefs.Save();
        //if(instance)
        //{
        //    instance.Gold = gold;
        //    MoneyUI.instance.RefreshUI();
        //}
    }

    private void Save()
    {
        //PlayerPrefs.SetInt("invenItems.Count", invenItems.Count);
        //for (int i = 0; i < invenItems.Count; i++)
        //{
        //    var saveItem = invenItems[i];
        //    PlayerPrefs.SetInt("invenItems.itemID" + i, saveItem.itemID);
        //    PlayerPrefs.SetInt("invenItems.count" + i, saveItem.count);
        //    PlayerPrefs.SetString("invenItems.getDate" + i, saveItem.getDate);
        //}
        //PlayerPrefs.SetInt("gold", Gold);
        //PlayerPrefs.SetInt("dia", dia);
    }
}