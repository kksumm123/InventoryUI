using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    public static ItemInfoUI instance;
    public ShopItemInfo shopItemInfo;
    private void Awake()
    {
        instance = this;
    }

    Text itemName;
    Image icon;
    Text description;
    Button button;
    GameObject shopBtn;
    GameObject invenBtn;
    private void Start()
    {
        itemName = transform.Find("Name").GetComponent<Text>();
        icon = transform.Find("Icon").GetComponent<Image>();
        description = transform.Find("Description").GetComponent<Text>();
        shopBtn = transform.Find("ShopBtn").gameObject;
        invenBtn = transform.Find("InvenBtn").gameObject;
        shopBtn.SetActive(false);
        invenBtn.SetActive(false);
        shopBtn.transform.Find("Button").GetComponent<Button>()
            .AddListener(this, ItemBuy);
        invenBtn.transform.Find("Button").GetComponent<Button>()
            .AddListener(this, ItemSell);
    }

    void ItemBuy()
    {
        print("ItemBuy");

        UserData.instance.gold -= shopItemInfo.buyPrice;
        var newItem = new InvenItemInfo();
        newItem.itemID = shopItemInfo.itemID;
        newItem.count = 1;
        newItem.getDate = DateTime.Now.ToString();
        UserData.instance.invenItems.Add(newItem);
    }
    void ItemSell()
    {
        print("ItemSell");
    }


    public void ShowShopItem(ShopItemInfo shopItemInfo)
    {
        shopBtn.SetActive(true);
        this.shopItemInfo = shopItemInfo;
        itemName.text = shopItemInfo.name;
        icon.sprite = shopItemInfo.icon;
        description.text = shopItemInfo.description;
    }
}
