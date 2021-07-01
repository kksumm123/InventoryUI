using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItemInfo
{
    [SerializeField] string name;
    [SerializeField] Sprite icon;
    [SerializeField] int sellPrice;
    [SerializeField] int butPrice;
    [SerializeField] string description;
}
public class ShopItemData : MonoBehaviour
{
    public List<ShopItemInfo> shopItems;
}

