using System;
using GameMechanics;
using UnityEngine;
using Pathfinding;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public float stopDistance;
        public float idleDistance;
        public float speed;
        
        protected float LastShotTime;
        protected Vector3 Direction;
        protected float Damage;
        public Transform target;
       
        private float _nextWaypointDistance;
        private Path _path;
        private int _currentWayPoint;
        private Seeker _seeker;

        protected void SetUpPathfinding()
        {
            _nextWaypointDistance = 3f;
            _seeker = GetComponent<Seeker>();
            _seeker.StartPath(transform.position, target.position, OnPathComplete);
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
                //Direction = PlayerMovement.Player.transform.position - transform.position;
                if (Direction.magnitude > stopDistance && Direction.magnitude < idleDistance )
                {
                    //transform.Translate(Direction.normalized * (speed * Time.deltaTime), Space.World);
                    var 
                    LastShotTime = Time.time;
                }
            }
        }
    }
}