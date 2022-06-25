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

        private Slider _sliderReference;

        private void Start()
        {
            currentHealth = maxHealth;
            _sliderReference = hpSlider.GetComponent<Slider>();
        }

        private void Update()
        {
            _sliderReference.value = (float)currentHealth / (float)maxHealth;
            hpSlider.transform.rotation = Quaternion.identity;
            if (currentHealth <= 0)
            {
                ItemManager.instance.DropRandomItem(transform.position, ItemManager.instance.monsterItems);
                Destroy(gameObject);
            }
        }
    }
}