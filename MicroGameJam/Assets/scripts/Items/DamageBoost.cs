using System.Collections;
using GameMechanics;
using UnityEngine;

[CreateAssetMenu(fileName = "damage potion", menuName = "item/damage potion")]
public class DamageBoost : Item
{
    public float damageIncrease;
    public float increaseDuration;
    public override void UseItem()
    {
        ItemManager.instance.StartCoroutine(ItemManager.instance.IncreaseDamage(damageIncrease, increaseDuration));
    }
}