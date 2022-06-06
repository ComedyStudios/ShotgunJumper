using System;
using UnityEngine;

namespace Enemies
{
    public class BulletScript : MonoBehaviour
    {
        public float damage;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                PlayerState.Instance.health -= (int)damage;
                Destroy(gameObject);
            }
        }
    }
}