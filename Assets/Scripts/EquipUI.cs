using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipUI : MonoBehaviour
{
    public EquipItem weaponEquipItem;
    public EquipItem armorEquipItem;
    public EquipItem potionEquipItem;

    public static EquipUI instance;
    private void Awake()
    {
        instance = this;
    }
    public void SetEquipItem(InvenItemInfo invenItemInfo)
    {
        ShopItemInfo shopItemInfo = invenItemInfo.GetShopItemInfo();
        switch (shopItemInfo.type)
        {
            case ItemType.Weapon:
                weaponEquipItem.SetItem(invenItemInfo);
                break;
            case ItemType.Armor:
                armorEquipItem.SetItem(invenItemInfo);
                break;
            case ItemType.Potion:
                potionEquipItem.SetItem(invenItemInfo);
                break;
            case ItemType.Etc:
                break;
        }

    }
}
