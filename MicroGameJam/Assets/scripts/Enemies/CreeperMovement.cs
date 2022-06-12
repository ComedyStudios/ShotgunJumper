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

        private void Start()
        {
            _state = GetComponent<EnemyState>();
            Damage = _state.damage;
            _renderer = GetComponent<SpriteRenderer>();
            SetUpPathfinding();
        }

        void Update() 
        {
            Move();
            
            //reset charge
            if (Direction.magnitude > chargeDistance)
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
                var damage = base.Damage * 1 / (Direction.magnitude * damageFallOff);
                PlayerState.Instance.health -= (int)base.Damage;
                Debug.Log($"creeper dealt {(int)base.Damage} damage to player");
                Destroy(gameObject);
            }
        }
    }
}