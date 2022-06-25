using UnityEngine;


    [CreateAssetMenu(fileName = "New HealingPotion", menuName = "item/Healing potion")]
    public class HealingPotion : Item
    {
        public int healAmount;

        public override void UseItem()
        {
            PlayerState.instance.health += healAmount;
            Debug.Log($"player was healed by {healAmount}");
        }
    }
