using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public List<Ray2D> rays = new List<Ray2D>();
    public float spread;
    public int rayCount;
    public int rayLength;
    public void Update()
    {
        rays.Clear();
        for (int i = 0; i< rayCount; i++)
        {
            rays.Add(new Ray2D(transform.position, transform.right + (i - rayCount/2) * spread* transform.up));
        }
        foreach (var ray in rays)
        {
            //TODO: add layer mask
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength,~LayerMask.GetMask("Player"));
            //Debug.Log(hit.collider.tag);
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
    
}
