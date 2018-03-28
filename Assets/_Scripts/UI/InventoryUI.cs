using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    private InventoryManager inventory;

    private InventorySlot[] slots;

    [SerializeField]
    private Transform selector;
    [SerializeField]
    private Transform slotParent;




	void Awake () 
    {
        inventory = InventoryManager.Instance;
        inventory.onItemChangedEvent += UpdateSlots;
        inventory.onSelectorChangedEvent += UpdateSelector;

        slots = slotParent.GetComponentsInChildren<InventorySlot>();

        inventory.space = slots.Length; //set space in inventory

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotIndex = i;
        }

        //UpdateSlots();
        //UpdateSelector();

	}




    private void UpdateSlots()  //I need to change this later, the player may want to choose a slot for each item
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].stackDisplay.transform.position = slots[i].transform.position;
            if (inventory.stacks[i] != null)
            {
                slots[i].UpdateSlot(inventory.stacks[i]); //add stack to the slot
            }
            else
            {
                slots[i].ClearSlot();       //else clear it
            }
        }
    }


	private void UpdateSelector()
	{
        selector.position = slots[inventory.selectedSlotID].transform.position;
	}
}
