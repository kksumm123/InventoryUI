using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] ShopItemUI itemBase;
    void Start()
    {
        foreach (var item in ShopItemData.instance.shopItems)
        {
            var newItem =
                Instantiate(itemBase, itemBase.transform.parent);
            newItem.Init(item);
        }
        itemBase.gameObject.SetActive(false);
    }
}
