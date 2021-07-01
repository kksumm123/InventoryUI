using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    Text name;
    Image itemInfoUI;
    Image icon;
    Text description;
    Button button;
    private void Start()
    {
        name = transform.Find("ItemInfoUI/Name").GetComponent<Text>();
        itemInfoUI = transform.Find("ItemInfoUI").GetComponent<Image>();
        icon = transform.Find("ItemInfoUI/Icon").GetComponent<Image>();
        description = transform.Find("ItemInfoUI/Description").GetComponent<Text>();
        button = transform.Find("ItemInfoUI/Button").GetComponent<Button>();
    }
}
