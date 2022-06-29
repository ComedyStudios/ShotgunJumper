using System;
using System.Collections;
using GameMechanics;
using MiniGameJam;
using UnityEngine;

namespace Enemies
{
    public class Grenade : MonoBehaviour
    {
        public GameObject grenadeExplosion;
        public float damageFallOff;
        [HideInInspector]
        public float damage;
        [HideInInspector]
        public float explosionRadius;
        [HideInInspector]
        public float timeTillExplosion;

        private void Start()
        {
            StartCoroutine(Explode());
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), AttackScript.Instance.gameObject.GetComponent<Collider2D>());
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
            var effect = Instantiate(grenadeExplosion, transform.position, Quaternion.identity);
            var localScale = effect.transform.localScale;
            localScale = new Vector3(localScale.x * explosionRadius, localScale.y * explosionRadius, 1); effect.transform.localScale = localScale;
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    var state = collider.gameObject.GetComponent<EnemyState>();
                    state.currentHealth -= (int)(damage * 1 / ((transform.position - collider.transform.position).magnitude * damageFallOff));
                }
                else if(collider.CompareTag("Player"))
                {
                    var state = collider.gameObject.GetComponent<PlayerState>();
                    state.health -= (int)damage;
                }
            }

            yield return new WaitForSeconds(0.75f);
            Destroy(effect);
            Destroy(gameObject);
        }
    }
}