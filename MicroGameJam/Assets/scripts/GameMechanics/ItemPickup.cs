using GameMechanics;
using Unity.VisualScripting;
using UnityEngine;

namespace MiniGameJam
{
    public class ItemPickup: MonoBehaviour
    {
        public Item item;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var o = collision.gameObject;
            if (o.CompareTag("Player") && Inventory.Instance.inventorySize > Inventory.Instance.inventory.Count)
            {
                Inventory.Instance.Add(item);
                Destroy(gameObject);
            }
        }
        
    }
}