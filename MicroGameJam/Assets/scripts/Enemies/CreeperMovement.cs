using System;
using System.Security.Cryptography;
using MiniGameJam;
using UnityEngine;

namespace Enemies
{
    public class CreeperMovement : Enemy
    {
        public float chargeDistance;
        public float chargeTime;
        public float damageFallOff;
        
        private float _lastChargeTime;
        private EnemyState _state;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _state = GetComponent<EnemyState>();
            damage = _state.damage;
            _renderer = GetComponent<SpriteRenderer>();
            SetUpPathfinding();
            _lastChargeTime = Time.time;
        }

        void Update() 
        {
            Move();
            
            //reset charge
            if (DistanceToPlayer() > chargeDistance)
            {
                _lastChargeTime = Time.time;
                _renderer.color = Color.yellow;
            }
            //start charging 
            else
            {
                _renderer.color = Color.red;
            }
            
            
            Attack();
        }

        private void Attack()
        {
            if (Time.time - _lastChargeTime >= chargeTime)
            {
                var damage = base.damage * 1 / (DistanceToPlayer() * damageFallOff);
                PlayerState.instance.health -= (int)(damage - Math.Clamp(PlayerState.instance.defense, 0, damage));
                Destroy(gameObject);
            }
        }
    }
}