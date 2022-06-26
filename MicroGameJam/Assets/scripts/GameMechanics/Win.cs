using System;
using Enemies;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace GameMechanics
{
    public class Win: MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && EnemyManager.instance.enemies.Count == 0)
            {
                var colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);
                foreach (var collider in colliders)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                       return;
                    }
                }
                MapGeneration.instace.hud.SetActive(false);
                MapGeneration.instace.winScreen.SetActive(true);
                AttackScript.Instance.gameObject.SetActive(false);
            }
        }
    }
}