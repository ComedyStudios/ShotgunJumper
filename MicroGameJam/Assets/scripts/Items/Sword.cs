using System.Collections.Generic;
using GameMechanics;
using MiniGameJam;
using UnityEngine;

[CreateAssetMenu(fileName = "UnnamedSword", menuName = "item/Weapon/Sword")]
public class Sword: Weapon
{
    
    
    public override void UseWeapon()
    {
        var anim = AttackScript.Instance.GetComponentInChildren<Animator>();
        anim.Play("slice",0, 0.0f);
        var rays = new List<Ray2D>();
        for (int i = 0; i< rayCount; i++)
        {
            var playerTransform = AttackScript.Instance.transform;
            rays.Add(new Ray2D(playerTransform.position,  Quaternion.Euler(0, 0, spread* (i-rayCount/2)) * playerTransform.right));
        }
        
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
        }
    }
}