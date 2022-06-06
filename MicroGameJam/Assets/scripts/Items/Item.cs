using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "item/create New Item")]
public class Item : ScriptableObject
{
    public string name;
    public float lifeTime;
    public Sprite icon;

    public virtual void UseItem()
    {
        Debug.Log("item has been used");
    }
}
