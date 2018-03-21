using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : ISingleton<InventoryManager> {

    protected InventoryManager() { }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();

    private int selectedSlot = 0;
    private InventorySlot[] slots;

    [SerializeField]
    private Transform selector;
    [SerializeField]
    private Transform slotParent;

    private int space;

	private void Start()
    {
        slots = slotParent.GetComponentsInChildren<InventorySlot>();
        space = slots.Length;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotIndex = i;
        }
        SelectSlot(0);
	}

	void Update()
	{
        //Get scrollwheel and update selected slot
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {

            if (selectedSlot < slots.Length - 1)
            {
                SelectSlot(selectedSlot + 1);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedSlot > 0)
            {
                SelectSlot(selectedSlot - 1);
            }
        }
	}

    public void SelectSlot(int slot)
    {
        selectedSlot = slot;
        selector.position = slots[slot].transform.position;
    }

    public void UseSlot()
    {
        Debug.Log("Use slot " + selectedSlot);
        slots[selectedSlot].UseItem();
    }

	public bool Add(Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Inventory is full");
            return false;
        }

        items.Add(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        return true;
    }
}
