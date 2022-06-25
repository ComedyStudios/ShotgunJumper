using MiniGameJam;
using UnityEngine;

namespace Enemies
{
    public class ZombieMovement : Enemy
    {
        public float hitRate;
        
        private float _lastHitTime;
        private EnemyState _state;

        private void Awake()
        {
            _state = GetComponent<EnemyState>();
            damage = _state.damage;
            SetUpPathfinding();
            _lastHitTime = Time.time;
        }

        void Update() 
        {
            Move();
            
            if (Time.time - _lastHitTime >= 1/hitRate && DistanceToPlayer() <= stopDistance)
            {
                _lastHitTime = Time.time;
                PlayerState.instance.health -= (int)damage;
            }
            
        }
    } 
}
    
