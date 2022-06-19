using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameMechanics
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance; 
        public List<Item> inventory = new List<Item>();
        public List<Weapon> weapons = new List<Weapon>();
        public int inventorySize;
        public GameObject invetoryUI;
        public GameObject itemUIElement;

        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            if (AttackScript.Instance.currentWeapon != null)
            {
                weapons.Add(AttackScript.Instance.currentWeapon);
            }
        }

        public void Add(Item item)
        {
            if (!(item is Weapon))
            {
                inventory.Add(item);
                var gameObject = Instantiate(itemUIElement, invetoryUI.transform);
                gameObject.GetComponent<RectTransform>().localPosition = new Vector3(40 + 80*((inventory.Count-1) % 4) ,  -50 -100*(float)Math.Floor((float)(inventory.Count / 5)), 0);
                gameObject.GetComponent<ItemUIComponent>().item = item;
            }
            else weapons.Add((Weapon) item);
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
}
