using System;
using System.Collections;
using MiniGameJam;
using UnityEngine;

namespace Enemies
{
    public class Grenade : MonoBehaviour
    {
        [HideInInspector]
        public float damage;
        [HideInInspector]
        public float explosionRadius;
        [HideInInspector]
        public float timeTillExplosion;

        private void Start()
        {
            StartCoroutine(Explode());
        }

        public void SetValues(float damage, float radius, float timeTillExplosion)
        {
            this.damage = damage;
            explosionRadius = radius;
            this.timeTillExplosion = timeTillExplosion;
        }
        

        private IEnumerator Explode()
        {
            yield return new WaitForSeconds(timeTillExplosion);
            var colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    var state = collider.gameObject.GetComponent<EnemyState>();
                    state.currentHealth -= (int)damage;
                }
                else if(collider.CompareTag("Player"))
                {
                    var state = collider.gameObject.GetComponent<PlayerState>();
                    state.health -= (int)damage;
                }
            }
            Destroy(gameObject);
        }
    }
}