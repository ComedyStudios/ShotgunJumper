using System;
using System.Collections.Generic;
using GameMechanics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemUIComponent : MonoBehaviour
    {
        public Item item;
        public TextMeshProUGUI text;
        public TextMeshProUGUI damageIncreaseText;
        public Image image;
        public bool canBeClicked = true;

        private Transform _parent;
        private Text _damageText;
        private void Start()
        {
            text.text = item.itemName;
            image.sprite = item.icon;
            _parent = transform.parent;
        }

        public void Update()
        {
            if (item is Weapon weapon)
            {
                damageIncreaseText.text = $"+{weapon.damageIncrease * 100}%";
            }
        }

        public void OnElementPressed()
        {
            if (canBeClicked)
            {
                item.UseItem();
                Inventory.instance.inventory.Remove(item);
                var count = Inventory.instance.inventory.Count;
            
                List<Transform> remainingItems = new List<Transform>();
                foreach (Transform child in _parent)
                {
                    if (child != this.transform)
                    {
                        remainingItems.Add(child);
                    }
                }

                for (int i = 0; i< remainingItems.Count; i++)
                {
                    remainingItems[i].localPosition = new Vector3(40 + 80*(i % 4) ,  -50 - 100*(float)Math.Floor((float)(i/4)), 0);
                }
                Destroy(gameObject);
            }
        }
    }
}
