using System;
using System.Collections;
using System.Collections.Generic;
using GameMechanics;
using MiniGameJam;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ItemUIComponent : MonoBehaviour
{
    public Item item;
    public TextMeshProUGUI text;
    public Image image;

    private Transform _parent;
    private void Start()
    {
        text.text = item.name;
        image.sprite = item.icon;
        _parent = transform.parent;
    }

    public void OnElementPressed()
    {
        item.UseItem();
        Inventory.Instance.inventory.Remove(item);
        var count = Inventory.Instance.inventory.Count;
        
        //TODO: fix this
        for (int i = 1; i<_parent.childCount; i++)
        {
            _parent.GetChild(i).localPosition = new Vector3(40 + 80*(i-1 % 4) ,  -50 - 100*(float)Math.Floor((float)(i/5)), 0);
        }
        Destroy(gameObject);
    }
}
