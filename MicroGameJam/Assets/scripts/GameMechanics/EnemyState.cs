using System;
using UnityEngine;

namespace MiniGameJam
{
    public class EnemyState: MonoBehaviour
    {
        public int maxHealth;
        public int damage;
        
        [HideInInspector]
        public int currentHealth;


        private void Start()
        {
            currentHealth = maxHealth;
           
        }

        private void Update()
        {
            if (currentHealth <= 0)
            {
                ItemManager.Instance.DropRandomItem(transform.position);
                Destroy(gameObject);
            }
        }
    }
}