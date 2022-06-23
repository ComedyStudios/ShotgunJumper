using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameMechanics
{
    public class Inventory : MonoBehaviour
    {
        
        public GameObject weaponUI;
        public List<Item> inventory = new List<Item>();
        public List<Weapon> weapons = new List<Weapon>();
        public GameObject inventoryUI;
        public GameObject itemUIElement;
        public int inventorySize;
        public float damageIncrease;
        public float weaponChangeTime;
        
        public static Inventory instance;

        private float _lastWeaponChange;
        

        public void Awake()
        {
            instance = this;
        }

        public void Start()
        {
            if (AttackScript.Instance.currentWeapon != null)
            {
                weapons.Add(AttackScript.Instance.currentWeapon);
            }
            AddNewWeapon(AttackScript.Instance.currentWeapon);
        }

        private void Update()
        {
            if (Time.time - _lastWeaponChange >= weaponChangeTime)
            {
                var uiTransform = weaponUI.transform;
                if (uiTransform.transform.childCount > 1 )
                {
                    uiTransform.GetChild(0).SetSiblingIndex(uiTransform.childCount-1);
                    var weaponScript = uiTransform.GetComponentInChildren<ItemUIComponent>();
                    weaponScript.item.UseItem();
                }
                _lastWeaponChange = Time.time;
            }
        }

        public void Add(Item item)
        {
            if (!(item is Weapon))
            {
                inventory.Add(item);
                var gameObject = Instantiate(itemUIElement, inventoryUI.transform);
                gameObject.GetComponent<RectTransform>().localPosition = new Vector3(40 + 80*((inventory.Count-1) % 4) ,  -50 -100*(float)Math.Floor((float)(inventory.Count / 5)), 0);
                gameObject.GetComponent<ItemUIComponent>().item = item;
            }
            else if (weapons.Contains((Weapon) item))
            {
                var weapon = (Weapon) item;
                weapon.damageIncrease += damageIncrease;
            }
            else
            {
                AddNewWeapon(item);
            };
        }

        private void AddNewWeapon(Item item)
        {
            var weapon = (Weapon) item;
            weapon.damageIncrease = 0;
            weapons.Add(weapon);
            var uiElement = Instantiate(itemUIElement, weaponUI.transform);
            uiElement.GetComponent<ItemUIComponent>().item = weapon;
            weapon.damageIncrease = 0;
        }

        public void Remove(Item item)
        {
            inventory.Remove(item);
        }

        public bool EnoughSpace()
        {
            if (instance.inventory.Count < inventorySize)
            {
                return true;
            }
            else return false;
        }
    }
}
