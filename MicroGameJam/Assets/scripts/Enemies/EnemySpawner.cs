using System;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public Enemy enemy;

        public void Awake()
        {
            Instantiate(enemy.gameObject, transform.position, Quaternion.identity); 
            Destroy(gameObject,1);
        }
    }
}