using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public void Init(ShopItemInfo item)
    {
        transform.Find("PriceText").GetComponent<Text>().text = item.buyPrice.ToString();
        GetComponent<Image>().sprite = item.icon;
    }
}
