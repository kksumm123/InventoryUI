using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    public static ItemInfoUI instance;
    private void Awake()
    {
        instance = this;
    }

    Text itemName;
    Image icon;
    Text description;
    Button button;
    private void Start()
    {
        itemName = transform.Find("Name").GetComponent<Text>();
        icon = transform.Find("Icon").GetComponent<Image>();
        description = transform.Find("Description").GetComponent<Text>();
        button = transform.Find("Button").GetComponent<Button>();
    }

    public void ShowShopItem(ShopItemInfo shopItemInfo)
    {
        itemName.text = shopItemInfo.name;
        icon.sprite = shopItemInfo.icon;
        description.text = shopItemInfo.description;
    }
}
