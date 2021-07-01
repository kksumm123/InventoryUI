using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour, IPointerClickHandler
{
    ShopItemInfo shopItemInfo;
    public void Init(ShopItemInfo item)
    {
        this.shopItemInfo = item;
        transform.Find("PriceText").GetComponent<Text>().text = item.buyPrice.ToString();
        GetComponent<Image>().sprite = item.icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemInfoUI.instance.ShowShopItem(shopItemInfo);
    }
}
