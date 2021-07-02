using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenItem : MonoBehaviour, IPointerClickHandler
{
    InvenItemInfo invenItemInfo;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            ItemInfoUI.instance.ShowInvenItem(invenItemInfo);
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // invenItemInfo 아이템을 장착하자 -> 장착정보

            // 긴 코드
            //ShopItemInfo shopItemInfo = 
            //    ShopItemData.instance.shopItems
            //    .Find(x => x.itemID == invenItemInfo.itemID);

            ShopItemInfo shopItemInfo = invenItemInfo.GetShopItemInfo();
        }
    }

    internal void Init(InvenItemInfo item)
    {
        invenItemInfo = item;

        //item.itemID
        //item.count
        ShopItemInfo shopItemInfo = invenItemInfo.GetShopItemInfo();
        GetComponent<Image>().sprite = shopItemInfo.icon;
        transform.Find("CountText").GetComponent<Text>().text = item.count.ToString();


    }
}
