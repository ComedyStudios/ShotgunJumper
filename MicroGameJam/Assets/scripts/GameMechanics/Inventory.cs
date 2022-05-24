using System;
using System.Collections;
using System.Collections.Generic;
using MiniGameJam;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance; 
    public List<Item> inventory = new List<Item>();
    public int inventorySize;
    public GameObject invetoryUI;
    public GameObject ItemUIElement;

    public void Awake()
    {
        Instance = this;
    }
    

    public void Add(Item item)
    {
        inventory.Add(item);
        var gameObject = Instantiate(ItemUIElement, invetoryUI.transform);
        
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(40 + 80*((inventory.Count-1) % 4) ,  -50 -100*(float)Math.Floor((float)(inventory.Count / 5)), 0);
        Debug.Log(inventory.Count);
            
        gameObject.GetComponent<ItemUIComponent>().item = item;
    }
    public void Remove(Item item)
    {
        inventory.Remove(item);
    }

    public bool EnoughSpace()
    {
        if (Instance.inventory.Count < inventorySize)
        {
            return true;
        }
        else return false;
    }
}
