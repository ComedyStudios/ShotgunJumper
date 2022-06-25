using System.Collections;
using GameMechanics;
using UnityEngine;

[CreateAssetMenu(fileName = "speed potion", menuName = "item/speed potion")]
public class SpeedPotion : Item
{
    public float speedIncrease;
    public float increaseDuration;
    public override void UseItem()
    {
        ItemManager.instance.StartCoroutine(ItemManager.instance.IncreaseSpeed(speedIncrease, increaseDuration));
    }
}