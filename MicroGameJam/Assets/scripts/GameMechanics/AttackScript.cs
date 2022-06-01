using System;
using System.Collections;
using System.Collections.Generic;
using MiniGameJam;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{
    private List<Ray2D> rays = new List<Ray2D>();
    public float spread;
    public int rayCount;
    public float rayLength;
    public float hitCooldown;
    public int damage;
    public InputAction attackAction;

    private float _lastHit;

    public void OnEnable()
    {
        attackAction.Enable();
    }

    public void OnDisable()
    {
        attackAction.Disable();
    }

    private void Start()
    {
        attackAction.performed += _ => DoDamage();
    }

    public void Update()
    {
        rays.Clear();
        for (int i = 0; i< rayCount; i++)
        {
            rays.Add(new Ray2D(transform.position,  Quaternion.Euler(0, 0, spread * (i-rayCount/2)) *transform.right));
        }
        foreach (var ray in rays)
        {
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength,~LayerMask.GetMask("Player", "Item"));
            if (hit == false)
            {
                Debug.DrawLine(ray.origin,ray.origin + ray.direction*rayLength, Color.red);
            }
            else
            {
                Debug.DrawLine(ray.origin,ray.origin + ray.direction*hit.distance, Color.red);
            }
        }
    }

    public void DoDamage()
    {
        //TODO: works for now maybe Expand later
        if (Time.time-_lastHit >= hitCooldown)
        {
            _lastHit = Time.time;
            List<GameObject> targetsInRange = new List<GameObject>();
            foreach (var ray in rays)
            {
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength,~LayerMask.GetMask("Player", "Item"));
                if (hit == true && !targetsInRange.Contains(hit.collider.gameObject))
                {
                    targetsInRange.Add(hit.collider.gameObject);
                }
            }
            foreach (var target in targetsInRange)
            {
                var state = target.GetComponent<EnemyState>();
                state.currentHealth -= damage;
                Debug.Log($"{target.name} has been hit, it has {state.currentHealth} health left");
            }
        }
    }
    
}
