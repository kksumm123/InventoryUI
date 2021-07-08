using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    public static ItemInfoUI instance;
    public ShopItemInfo shopItemInfo;
    public InvenItemServer invenItemInfo;
    private void Awake()
    {
        instance = this;
    }

    Text itemName;
    Image icon;



    Text description;
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

        var newItem = new InvenItemServer();
        newItem.ID = shopItemInfo.itemID;
        newItem.Count = 1;
        newItem.GetDate = DateTime.Now;

        UserData.instance.BuyItem(shopItemInfo.buyPrice, newItem);
        InvenUI.instance.RefreshUI();
        MoneyUI.instance.RefreshUI();
    }
    void ItemSell()
    {
        print("ItemSell");

        UserData.instance.SellItem(shopItemInfo.sellPrice, invenItemInfo);
        InvenUI.instance.RefreshUI();
        MoneyUI.instance.RefreshUI();
    }


    public void ShowShopItem(ShopItemInfo shopItemInfo)
    {
        shopBtn.SetActive(true);
        invenBtn.SetActive(false);

        SetItemInfo(shopItemInfo);
    }
    internal void ShowInvenItem(InvenItemServer invenItemInfo)
    {
        this.invenItemInfo = invenItemInfo;
        invenBtn.SetActive(true);
        shopBtn.SetActive(false);

        var shopItemInfo = invenItemInfo.GetShopItemInfo();
        SetItemInfo(shopItemInfo);
    }


    private void SetItemInfo(ShopItemInfo shopItemInfo)
    {
        this.shopItemInfo = shopItemInfo;
        itemName.text = shopItemInfo.name;
        icon.sprite = shopItemInfo.Icon;
        description.text = shopItemInfo.description;
    }

}
