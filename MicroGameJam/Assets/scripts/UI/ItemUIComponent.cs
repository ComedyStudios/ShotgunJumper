using System;
using System.Collections;
using System.Collections.Generic;
using MiniGameJam;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIComponent : MonoBehaviour
{
    public Item item;
    public TextMeshProUGUI text;
    public Image image;

    private void Start()
    {
        text.text = item.name;
        image.sprite = item.icon;
    }
}
