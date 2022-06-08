using GameMechanics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "item/Weapon")]
public class Weapon: Item
{
    public float spread;
    public int rayCount;
    public float rayLength;
    public float hitCooldown;
    public int damage;

    public override void UseItem()
    {
        AttackScript.Instance.SetWeapon(this);
    }

    public virtual void Shoot()
    {
        
    }
}
