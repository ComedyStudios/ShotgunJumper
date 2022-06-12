using System;
using UnityEditor.Rendering;
using UnityEngine;

namespace Enemies
{
    public class ArcherMovement : Enemy
    {
        public GameObject bulletPrefab;
        public float bulletSpeed;
        public float hitCooldown;

        private void Start()
        {
            SetUpPathfinding();
        }

        private void Update()
        {
            Move();
            Attack();
        }

        private void Attack()
        {
            
            var angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            if (Time.time - LastShotTime >= hitCooldown)
            {
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
                var bulletRb = bullet.GetComponent<Rigidbody2D>();
                var bulletScript = bullet.GetComponent<BulletScript>();
                bulletScript.damage = this.Damage;
                bulletRb.velocity = bullet.transform.right * bulletSpeed;
                LastShotTime = Time.time;
            }
        }
    }
}