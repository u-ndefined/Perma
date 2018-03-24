using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Items/Consumable")]
public class Consumable : Item {

	public override void Use()
	{
        base.Use();
        /*
        InventoryManager.Instance.Remove(this, 1);
        Debug.Log("quantity " + quantity);
        */
	}
}
