using System.Security.Cryptography;
using MiniGameJam;
using UnityEngine;

namespace Enemies
{
    public class CreeperMovement : MonoBehaviour
    {
        public float speed;
        public float idleDistance;
        public float chargeDistance;
        public float stopDistance;
        public float chargeTime;
        public float damageFallOff;
        
        private int _damage;
        private float _lastChargeTime;
        private EnemyState _state;
        private SpriteRenderer _renderer;

        private void Start()
        {
            _state = GetComponent<EnemyState>();
            _damage = _state.damage;
            _renderer = GetComponent<SpriteRenderer>();
        }

        void Update() 
        {
            var direction = PlayerMovement.player.transform.position - transform.position;
            if (direction.magnitude > stopDistance && direction.magnitude < idleDistance)
            {
                transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
            }
            if (direction.magnitude > chargeDistance)
            {
                _lastChargeTime = Time.time;
                _renderer.color = Color.yellow;
            }
            else
            {
                _renderer.color = Color.red;
            }

            if (Time.time - _lastChargeTime >= chargeTime)
            {
                //TODO: make the creeper die afther exlopsion ergo it only hits the player once
                var damage = _damage * 1 / (direction.magnitude * damageFallOff);
                PlayerState.Instance.health -= (int) damage;
                Debug.Log($"creeper dealt {(int)damage} damage to player");
                Destroy(gameObject);
            }
        }
    }
}