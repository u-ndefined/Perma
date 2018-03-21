using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory")]
public class Item : ScriptableObject {
    new public string name = "New item";
    public Sprite icon = null;
    private int quantity;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
}
