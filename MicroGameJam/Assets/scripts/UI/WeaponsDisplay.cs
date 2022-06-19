using System;
using System.Collections.Generic;
using System.Linq;
using GameMechanics;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class WeaponsDisplay : MonoBehaviour
    {
        public GameObject uiComponentPrefab;
        public float weaponChangeTime;
        private List<Weapon> _weapons = new List<Weapon>();
        private float _lastWeaponChange;
        private int _registeredWeapons = 0;
        
        private void Update()
        {
            if (_registeredWeapons !=Inventory.Instance.weapons.Count )
            {
                foreach (var weapon in Inventory.Instance.weapons)
                {
                    if (!_weapons.Contains(weapon))
                    {
                        var uiElement = Instantiate(uiComponentPrefab, this.transform);
                        uiElement.GetComponent<ItemUIComponent>().item = weapon;
                        _weapons = Inventory.Instance.weapons.ToList();
                    }
                    else
                    {
                        //TODO: Increase damage
                    }
                }
                _registeredWeapons = Inventory.Instance.weapons.Count; 
            }

            if (Time.time - _lastWeaponChange >= weaponChangeTime)
            {
                if (transform.childCount > 1 )
                {
                    transform.GetChild(0).SetSiblingIndex(transform.childCount-1);
                    var weaponScript = transform.GetComponentInChildren<ItemUIComponent>();
                    weaponScript.item.UseItem();
                }
                _lastWeaponChange = Time.time;
            }
        }
    }
}