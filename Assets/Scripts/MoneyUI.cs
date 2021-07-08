﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public static MoneyUI instance;
    Text goldText;
    Text diaText;
    private void Awake()
    {
        instance = this;
        goldText = transform.Find("Gold/Text").GetComponent<Text>();
        diaText = transform.Find("Dia/Text").GetComponent<Text>();
    }
    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        goldText.text = UserData.instance.userDataServer.Gold.ToString();
        diaText.text = UserData.instance.userDataServer.Dia.ToString();
    }
}
