using MiniGameJam;
using UnityEngine;

namespace Enemies
{
    public class ArcherMovement : Enemy
    {
        public GameObject bulletPrefab;
        public float bulletSpeed;
        public float hitCooldown;

        private EnemyState _state;
        private void Awake()
        {
            SetUpPathfinding();
            _state = GetComponent<EnemyState>();
            damage = _state.damage;
            lastShotTime = Time.time;
        }

        private void Update()
        {
            Move();
            Attack();
        }

        private void Attack()
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (Time.time - lastShotTime >= hitCooldown)
            {
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
                var bulletRb = bullet.GetComponent<Rigidbody2D>();
                var bulletScript = bullet.GetComponent<BulletScript>();
                bulletScript.damage = damage;
                bulletRb.velocity = bullet.transform.right * bulletSpeed;
                lastShotTime = Time.time;
            }
        }
    }
}