using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    private InventoryManager inventory;



    private InventorySlot[] slots;



	void Start () 
    {
        inventory = InventoryManager.Instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = inventory.slots;
	}
	



    void UpdateUI()
    {
        Debug.Log("update UI " + inventory.slots.Length);
        Debug.Log("items count " + inventory.items.Count);
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                Debug.Log("add?");
                inventory.slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                inventory.slots[i].ClearSlot();
            }
        }
    }
}
