using MiniGameJam;
using UnityEngine;

namespace Enemies
{
    public class ZombieMovement : Enemy
    {
        public float hitRate;
        
        private float _lastHitTime;
        private EnemyState _state;

        private void Start()
        {
            _state = GetComponent<EnemyState>();
            Damage = _state.damage;
            SetUpPathfinding();
        }

        void Update() 
        {
            Move();
            
            if (Time.time - _lastHitTime >= 1/hitRate && DistanceToPlayer() <= stopDistance)
            {
                _lastHitTime = Time.time;
                PlayerState.Instance.health -= (int)Damage;
            }
            
        }
    } 
}
    
