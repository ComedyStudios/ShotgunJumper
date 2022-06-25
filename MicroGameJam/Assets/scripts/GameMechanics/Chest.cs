using System;
using System.Collections.Generic;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace GameMechanics
{
    public class Chest : MonoBehaviour
    {
        public InputAction openChestAction;
        public int randomItemsInChest;
        public List<Item> setItemsInChest;
        public float minDistanceToChest;


        private void OnEnable()
        {
            openChestAction.Enable();
        }

        private void OnDisable()
        {
            openChestAction.Disable();
        }

        private void Start()
        {
            openChestAction.performed += _ => OpenChest();
        }

        private void OpenChest()
        {
            if ((AttackScript.Instance.transform.position - transform.position).magnitude < GetComponent<CircleCollider2D>().radius)
            {
                for (int i = 0; i < randomItemsInChest; i++)
                {
                    ItemManager.instance.DropRandomItem(this.transform.position + 0.5f*(Vector3)Random.insideUnitCircle, ItemManager.instance.chestItems);
                }

                foreach (var item in setItemsInChest)
                {
                    ItemManager.instance.DropSetItem(item, this.transform.position + 0.5f*(Vector3)Random.insideUnitCircle);
                }
                Destroy(gameObject);
            }
        }
    }
}