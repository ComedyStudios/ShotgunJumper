using System;
using GameMechanics;
using MiniGameJam;
using UnityEngine;
using Pathfinding;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public float stopDistance;
        public float idleDistance;
        public float speed;
        public Transform target;
        
        protected float damage;
        protected float lastShotTime;
        protected Vector3 direction;

        private Path _pathingRate;
        private float _lastTimePathed;
        private Path _path;
        private int _currentWayPoint;
        private Seeker _seeker;
        private bool _playerOutOfRange;

        protected void SetUpPathfinding()
        {
            target = GameObject.FindWithTag("Player").transform;
            _seeker = GetComponent<Seeker>();
            _seeker.StartPath(transform.position, target.position, OnPathComplete);
            InvokeRepeating(nameof(UpdatePath), 0f, .5f);
        }

        private void UpdatePath()
        {
            if (_seeker.IsDone())
            {
                _seeker.StartPath(transform.position, target.position, OnPathComplete);
            }
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                _path = p;
                _currentWayPoint = 0;
            }
        }
        
        protected void Move()
        {

            if (_path != null)
            {
                direction = PlayerMovement.playerInstance.gameObject.transform.position - transform.position;
                var newDistance = DistanceToPlayer();
                if ( newDistance < idleDistance)
                {
                    if (newDistance > stopDistance)
                    {
                        var moveDirection = (Vector2)(_path.vectorPath[_currentWayPoint] - transform.position).normalized;
                        transform.Translate(moveDirection * (speed * Time.deltaTime), Space.World);
                        lastShotTime = Time.time;
                    }
                    
                    if (_playerOutOfRange)
                    {
                        _playerOutOfRange = false;
                        EnemyManager.instance.enemies.Add(gameObject);
                    }
                }
                else if(!_playerOutOfRange)
                {
                    EnemyManager.instance.enemies.Remove(gameObject);
                    _playerOutOfRange = true;
                }
                var distanceToNextPoint = Vector2.Distance(transform.position, _path.vectorPath[_currentWayPoint]);
                if (distanceToNextPoint < 0.1f && _currentWayPoint < _path.vectorPath.Count-1)
                {
                    _currentWayPoint++;
                }
            }
        }

        protected float DistanceToPlayer()
        {
            var newDistance = 0f;
            Vector3 lastVector = transform.position;
            if (_path != null)
            {
                foreach (var vector3 in _path.vectorPath)
                {
                    newDistance += Vector2.Distance(lastVector, vector3);
                    lastVector = vector3;
                }
            }
            return newDistance;
        }
    }
}