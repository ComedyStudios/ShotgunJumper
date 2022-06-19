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
