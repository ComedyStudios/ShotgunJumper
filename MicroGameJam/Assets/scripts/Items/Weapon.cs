using GameMechanics;
using UnityEngine;

public abstract class Weapon: Item
{
    public float spread;
    public int rayCount;
    public float rayLength;
    public float hitCooldown;
    public float damage;

    public override void UseItem()
    {
        AttackScript.Instance.currentWeapon = this;
    }

    public virtual void UseWeapon()
    {
        
    }
}
