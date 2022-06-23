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
            if (o.CompareTag("Player") && Inventory.instance.inventorySize > Inventory.instance.inventory.Count)
            {
                Inventory.instance.Add(item);
                Destroy(gameObject);
            }
        }
        
    }
}