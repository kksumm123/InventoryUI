using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SetItem(InvenItemInfo invenItemInfo)
    {
        ShopItemInfo shopItemInfo = invenItemInfo.GetShopItemInfo();

        Image image = transform.Find("Icon").GetComponent<Image>();
        image.sprite = shopItemInfo.icon;
    }
}
