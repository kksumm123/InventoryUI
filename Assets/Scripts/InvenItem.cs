using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenItem : MonoBehaviour, IPointerClickHandler
{
    InvenItemServer invenItemInfo;
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

            // 짧은 코드
            //ShopItemInfo shopItemInfo = invenItemInfo.GetShopItemInfo();
            EquipUI.instance.SetEquipItem(invenItemInfo);
        }
    }

    internal void Init(InvenItemServer item)
    {
        invenItemInfo = item;

        //item.itemID
        //item.count
        ShopItemInfo shopItemInfo = invenItemInfo.GetShopItemInfo();
        GetComponent<Image>().sprite = shopItemInfo.Icon;
        transform.Find("CountText").GetComponent<Text>().text = item.Count.ToString();


    }
}
