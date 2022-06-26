using UnityEngine;

[CreateAssetMenu(fileName = "defense potion", menuName = "item/defense potion")]
public class DefensePotion : Item
{
    public float defenseIncrease;
    public float increaseDuration;
    public override void UseItem()
    {
        ItemManager.instance.StartCoroutine(ItemManager.instance.IncreaseDefense(defenseIncrease, increaseDuration));
    }
}