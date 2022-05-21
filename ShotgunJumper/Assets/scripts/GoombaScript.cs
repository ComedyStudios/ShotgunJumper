using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaScript : MonoBehaviour
{
    public float damage;
    public float hp;
    public float speed;
    public GameObject player;
    public float rayDistance; 
    
    private Rigidbody2D _rb;
    private float _dir = 1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveGoomba();
        void CheckForSteps()
        {
        }
    }

    void MoveGoomba()
    {
        var ray = Physics2D.Raycast(transform.position, Vector2.right * _dir, rayDistance, ~(LayerMask.GetMask("Enemy")+LayerMask.GetMask("Player")));
        if (ray)
        {
            _dir *= -1; 
        }
        _rb.velocity = new Vector2(speed * _dir, _rb.velocity.y);
        Debug.DrawLine(transform.position, transform.position + Vector3.right * _dir);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionObject = collision.gameObject;
        switch (collisionObject.transform.tag)
        {
            case "Player":
                var playerScript = collisionObject.GetComponent<PlayerMovement>();
                playerScript.hp -= damage;
                break;
            case "Bullet":
                var bulletScript = collisionObject.GetComponent<BulletScript>();
                hp -= bulletScript.damage;
                if (hp < 0)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
}
