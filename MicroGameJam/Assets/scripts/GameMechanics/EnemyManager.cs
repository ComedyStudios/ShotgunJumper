using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameMechanics
{
    public class EnemyManager : MonoBehaviour
    {
        public List<GameObject> enemies = new List<GameObject>();
        public static EnemyManager instance;

        private void Awake()
        {
            instance = this;
        }
    }
}