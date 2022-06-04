﻿using UnityEngine;

namespace MiniGameJam
{
    [CreateAssetMenu(fileName = "New item", menuName = "item/create New Item")]
    public class Item : ScriptableObject
    {
        public string name;
        public float lifeTime;
        public Sprite icon;
    }
}