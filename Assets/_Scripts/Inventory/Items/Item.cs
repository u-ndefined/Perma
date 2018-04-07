using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject {
    new public string name = "New item";
    public Sprite icon = null;

    public virtual void Use()
    {
        Debug.Log("Using " + name);

    }
}
