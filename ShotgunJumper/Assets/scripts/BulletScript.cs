using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 _startposition;
    public float damage;
    public float range;
    
    
    public void Fire(float speed, float damage, float range)
    {
        _startposition = transform.position;
        var rb = this.GetComponent<Rigidbody2D>();
        this.damage = damage;
        this.range = range;
        rb.AddForce(this.transform.right * speed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if ((transform.position-_startposition).magnitude > range)
        {
            Destroy(this.gameObject);
        }
    }
}
