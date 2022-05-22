using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance; 
    public List<Item> inventory = new List<Item>();
    public int inventorySize;
    public Item item;

    public void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        inventory.Add(item);
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
