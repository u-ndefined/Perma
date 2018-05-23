using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    SEED,
    SHOVEL,
    AXE,
    FRUIT
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject {
    new public string name = "New item";
    public Sprite icon = null;
    public GameObject objectOnGround;
    public ItemType itemType;
    public int maxQuantity = 99;
    [TextArea(3, 20)]
    public string description;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

}