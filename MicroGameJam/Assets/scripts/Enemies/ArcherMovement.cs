using System;
using UnityEditor.Rendering;
using UnityEngine;

namespace Enemies
{
    public class ArcherMovement : MonoBehaviour
    {
        public float speed;
        public float idleDistance;
        public float stopDistance;
        public GameObject bulletPrefab;
        public float bulletSpeed;
        public float hitCooldown;
        public float damage;
        
        private float _lastShotTime;
        private void Update()
        {
            var direction = PlayerMovement.player.transform.position - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (direction.magnitude > stopDistance && direction.magnitude < idleDistance)
            {
                transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
                _lastShotTime = Time.time;
            }
            if(Time.time - _lastShotTime >= hitCooldown)
            {
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
                var bulletRb = bullet.GetComponent<Rigidbody2D>();
                var bulletScript = bullet.GetComponent<BulletScript>();
                bulletScript.damage = this.damage;
                bulletRb.velocity = bullet.transform.right * bulletSpeed;
                _lastShotTime = Time.time;
            }
        }
    }
}