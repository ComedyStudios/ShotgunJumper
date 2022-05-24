﻿using UnityEngine;

namespace MiniGameJam.Items
{
    [CreateAssetMenu(fileName = "New HealingPotion", menuName = "item/create New healing potion")]
    public class HealingPotion : Item
    {
        public float healAmount;
    }
}