﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenItem : MonoBehaviour, IPointerClickHandler
{
    InvenItemInfo inveniteminfo;
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemInfoUI.instance.ShowInvenItem(inveniteminfo);
    }

    internal void Init(InvenItemInfo item)
    {
        inveniteminfo = item;

        //item.itemID
        //item.count
        ShopItemInfo shopItemInfo =
            ShopItemData.instance.shopItems
            .Find(x => x.itemID == item.itemID);
        GetComponent<Image>().sprite = shopItemInfo.icon;
        transform.Find("CountText").GetComponent<Text>().text = item.count.ToString();


    }
}
