using System;
using Enemies;
using GameMechanics;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Grenade Launcher", menuName = "item/Weapon/Grenade Launcher")]
public class GrenadeLauncher: Weapon
{
    public float throwingForce;
    public float radius;
    public float timeTillExplosion;
    public GameObject grenadePrefab;
    public override void UseWeapon()
    {
        var playerTransform = AttackScript.Instance.transform;
        var grenade = Instantiate(grenadePrefab, playerTransform.position, playerTransform.rotation);
        var grenadeRb = grenade.GetComponent<Rigidbody2D>();
        var grenadeScript = grenadeRb.GetComponent<Grenade>();
        grenadeScript.SetValues(damage * (1+ damageIncrease), radius,timeTillExplosion);
        grenadeRb.AddForce(playerTransform.right * throwingForce);
    }
}