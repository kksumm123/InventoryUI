using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenUI : MonoBehaviour
{
    public static InvenUI instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] InvenItem itemBase;
    void Start()
    {
        RefreshUI();
    }

    List<GameObject> childItem = new List<GameObject>();
    public void RefreshUI()
    {
        childItem.ForEach(x => Destroy(x));
        childItem.Clear();
        itemBase.gameObject.SetActive(true);
        foreach (var item in UserData.instance.invenItems)
        {
            var newItem =
                Instantiate(itemBase, itemBase.transform.parent);
            newItem.Init(item);
            childItem.Add(newItem.gameObject);
        }
        itemBase.gameObject.SetActive(false);
    }
}
