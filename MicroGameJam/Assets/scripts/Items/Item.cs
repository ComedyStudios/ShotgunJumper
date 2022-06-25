using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "item/create New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public virtual void UseItem()
    {
        Debug.Log("item has been used");
    }
}
