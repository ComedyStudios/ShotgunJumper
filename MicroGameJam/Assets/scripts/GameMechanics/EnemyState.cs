﻿using System;
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
            if (currentHealth == 0)
            {
                //TODO: Kill enemy and drop Stuff
            }
        }
    }
}