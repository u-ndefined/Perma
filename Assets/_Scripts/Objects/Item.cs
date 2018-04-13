using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    SEED,
    SHOVEL,
    AXE,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject {
    new public string name = "New item";
    public Sprite icon = null;
    public GameObject objectOnGround;
    public ItemType itemType;

    public virtual void Use()
    {
        Debug.Log("Using " + name);

    }

    public virtual void UseOn(Interactable target)
    {
        
    }
}