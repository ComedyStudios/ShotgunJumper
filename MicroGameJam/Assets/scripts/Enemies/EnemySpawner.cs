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
            Debug.Log($"created {enemy.name} at {transform.position}");
            Destroy(gameObject,1);
        }
    }
}