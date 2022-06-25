using System;
using MiniGameJam;
using UnityEngine;

namespace Enemies
{
    public class BulletScript : MonoBehaviour
    {
        public float damage;
        public string enemyTag;
        public float lifeTime;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(enemyTag))
            {
                if (!other.TryGetComponent(out EnemyState state))
                {
                    PlayerState.instance.health -= (int)damage;
                }
                else state.currentHealth -= (int)damage;
                Destroy(gameObject);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Map"))
            {
                Destroy(gameObject);
            }
        }
    }
}