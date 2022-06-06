using System;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGameJam
{
    public class EnemyState: MonoBehaviour
    {
        public int maxHealth;
        public int damage;
        public Canvas hpSlider;
        
        [HideInInspector]
        public int currentHealth;


        private void Start()
        {
            currentHealth = maxHealth;
           
        }

        private void Update()
        {
            hpSlider.GetComponent<Slider>().value = (float)currentHealth / (float)maxHealth;
            hpSlider.transform.rotation = Quaternion.identity;
            if (currentHealth <= 0)
            {
                ItemManager.Instance.DropRandomItem(transform.position);
                Destroy(gameObject);
            }
        }
    }
}