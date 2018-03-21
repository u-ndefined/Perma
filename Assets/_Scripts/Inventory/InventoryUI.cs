using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    private InventoryManager inventory;



    public InventorySlot[] slots;



	void Start () 
    {
        inventory = InventoryManager.Instance;
        inventory.onItemChangedCallback += UpdateUI;



	}
	



    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
