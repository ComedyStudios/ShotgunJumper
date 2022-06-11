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

        private ButtonPrompt _buttonPrompt;
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
            _buttonPrompt = GetComponent<ButtonPrompt>();
        }

        private void OpenChest()
        {
            for (int i = 0; i < randomItemsInChest; i++)
            {
                ItemManager.Instance.DropRandomItem(this.transform.position + 0.5f*(Vector3)Random.insideUnitCircle);
            }

            foreach (var item in setItemsInChest)
            {
                ItemManager.Instance.DropSetItem(item, this.transform.position + 0.5f*(Vector3)Random.insideUnitCircle);
            }
            Destroy(gameObject);
        }
        
    }
}