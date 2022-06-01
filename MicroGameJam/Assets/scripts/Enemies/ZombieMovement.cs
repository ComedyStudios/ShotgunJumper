using System;
using System.Collections;
using System.Collections.Generic;
using MiniGameJam;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float speed;
    public float minDistanceToPlayer;
    public float maxDistanceToPlayer;
    public float hitRate;

    private int _damage;
    private float _lastHitTime;
    private EnemyState _state;

    private void Start()
    {
        _state = GetComponent<EnemyState>();
        _damage = _state.damage;
    }

    void Update() 
    {
        var direction = PlayerMovement.Player.transform.position - transform.position;
        if (direction.magnitude > minDistanceToPlayer && direction.magnitude < maxDistanceToPlayer)
        {
            transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
            _lastHitTime = Time.time;
        }
        else
        {
            if (Time.time - _lastHitTime >= 1/hitRate && direction.magnitude < maxDistanceToPlayer)
            {
                _lastHitTime = Time.time;
                PlayerState.Instance.health -= _damage;
                Debug.Log($"{this.name} hit player");
            }
        }
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
