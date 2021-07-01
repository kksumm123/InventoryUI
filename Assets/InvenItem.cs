using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Init(InvenItemInfo item)
    {
        //item.itemID
        //item.count
        ShopItemInfo shopItemInfo = 
            ShopItemData.instance.shopItems
            .Find(x => x.itemID == item.itemID);
        GetComponent<Image>().sprite = shopItemInfo.icon;
        transform.Find("CountText").GetComponent<Text>().text = item.count.ToString();


    }
}
