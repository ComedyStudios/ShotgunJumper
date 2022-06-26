using System;
using System.Collections.Generic;
using Enemies;
using GameMechanics;
using UnityEngine;

[CreateAssetMenu(fileName = "UnnamedGun", menuName = "item/Weapon/Gun")]
public class Gun : Weapon
{
    public GameObject bulletPrefab;
    public float bulletSpeed;

    
    public override void UseWeapon()
    {
        for (int i = 0; i< rayCount; i++)
        {
            var playerTransform = AttackScript.Instance.transform;
            var bullet = Instantiate(bulletPrefab, playerTransform.position, Quaternion.Euler(0,0,spread* (i-rayCount/2) + playerTransform.eulerAngles.z));
            var bulletRb = bullet.GetComponent<Rigidbody2D>();
            var bulletScript = bullet.GetComponent<BulletScript>();
            bulletScript.damage = damage * (1+ damageIncrease);
            bulletRb.velocity = bullet.transform.right * bulletSpeed;
        }
    }
}
