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
}

public class UserData : MonoBehaviour
{
    public static UserData instance;

    public List<InvenItemInfo> invenItems;

    public int gold;
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
            gold = PlayerPrefs.GetInt("gold");
            dia = PlayerPrefs.GetInt("dia");
        }
        else
        {
            gold = 1100;
            dia = 120;
        }
    }
    private void Save()
    {
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.SetInt("dia", dia);
    }
}