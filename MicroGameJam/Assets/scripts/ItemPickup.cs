using UnityEngine;

namespace DefaultNamespace
{
    public class ItemPickup: MonoBehaviour
    {
        public Item item;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var o = collision.gameObject;
            if (o.CompareTag("Player"))
            {
                Inventory.Instance.Add(item);
                Destroy(gameObject);
            }
        }
    }
}