using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : ISingleton<InventoryManager>
{

    protected InventoryManager()
    {
    }

    public delegate void voidNoParam();
    public voidNoParam onItemChangedEvent;
    public voidNoParam onSelectorChangedEvent;

    public Stack[] stacks;

    public int selectedSlotID = 0;

    public int space = 10;

    public Stack stackUsed;

    private bool init = false;


    void Awake()
    {
        stacks = new Stack[space];
        for (int i = 0; i < space; i++)
        {
            stacks[i].Clear();
        }


    }

	private void Start()
	{
        Invoke("SelectZero", 0.3f);
	}

	private void SelectZero()
    {
        SelectSlot(0);
    }

	
	
	void Update()
    {
        //Get scrollwheel and update selected slot
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {

            if (selectedSlotID < space - 1)
            {
                SelectSlot(selectedSlotID + 1);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedSlotID > 0)
            {
                SelectSlot(selectedSlotID - 1);
            }
        }
    }

    public void SelectSlot(int slotID)
    {
        ResetSlotUsed();

        selectedSlotID = slotID;

        if (onSelectorChangedEvent != null)         //update selector
        {
            onSelectorChangedEvent.Invoke();
        }
    }

    public bool UseSlot()
    {
        if(!stacks[selectedSlotID].empty)
        {
            Debug.Log("Use slot " + selectedSlotID);
            stackUsed = stacks[selectedSlotID];
            return true;
        }
        return false;
       
    }

	public void ResetSlotUsed()
	{
        stackUsed.Clear();
	}

	public void DropItem(Stack dropStack)
    {
        Transform player = PlayerControler.Instance.transform;
        Vector3 dropPosition = player.position + (player.forward * 1.2f) + (Vector3.up * 0.1f);
        Quaternion rot = player.rotation;
        if (dropStack.item.itemType == ItemType.SHOVEL)
        {
            rot = Quaternion.Euler(90, 0, 90);
        }
        GameObject dropObject = Instantiate(dropStack.item.objectOnGround, dropPosition, rot);
        dropObject.GetComponent<ItemPickup>().stack.quantity = dropStack.quantity;
    }

    #region Add_and_Remove_at_index
    public Stack AddAtIndex(int index, Stack addedStack)
    {
        if (index < 0 || index >= stacks.Length) return Stack.Empty();    //if wrong index return

        bool done = false;

        if (stacks[index].empty)                
        {
            addedStack = stacks[index].SafeAddStack(addedStack, false);        //if empty force add
            done = true;
        }
        if (!done && stacks[index].item == addedStack.item)                             
        {
            addedStack = stacks[index].SafeAddStack(addedStack);                //if same item safe add
            done = true;
        }

        if (onItemChangedEvent != null)                             //updateUI
        {
            onItemChangedEvent.Invoke();
        }

        return addedStack;
     
    }

    public Stack Replace(int slotIndex, Stack stack)
    {
        Stack replacedStack = stacks[slotIndex];
        stacks[slotIndex] = stack;

        if (onItemChangedEvent != null)         //updateUI
        {
            onItemChangedEvent.Invoke();
        }
        return replacedStack;
    }


    public bool RemoveAtIndex(int index, int removedQuantity)
    {
        
        if (stacks[index].quantity > removedQuantity) //if enought quantity remove quantity
        {
            stacks[index].quantity -= removedQuantity;
        }
        else if (stacks[index].quantity == removedQuantity)  //if exact quantity remove stack
        {
            stacks[index].Clear();
        }
        else
        {
            Debug.Log("!!! " + removedQuantity + " remaining to remove");
            return false;
        }

        if (onItemChangedEvent != null)         //updateUI
        {
            onItemChangedEvent.Invoke();
        }
        return true;
    }

    #endregion


    #region Add_and_Remove

    public Stack Add(Stack addedStack)
    {


        for (int i = 0; i < stacks.Length; i++)
        {
            addedStack = stacks[i].SafeAddStack(addedStack);    //add first on none empty slot
        }
        for (int i = 0; i < stacks.Length; i++)
        {
            addedStack = stacks[i].SafeAddStack(addedStack, false);   //then empty
        }

        if (onItemChangedEvent != null)         //updateUI
        {
            onItemChangedEvent.Invoke();
        }

        return addedStack;
    }

    public bool Remove(Stack removedStack)
    {
        
        if(GetQuantity(removedStack) >= removedStack.quantity )                                                //if enought
        {
            for (int i = 0; i < stacks.Length; i++)
            {
                removedStack = stacks[i].RemoveStack(removedStack);    //remove
            }

            if (onItemChangedEvent != null)         //updateUI
            {
                onItemChangedEvent.Invoke();
            }

            return true;
        }

        return false;

    }
    #endregion

    public int GetQuantity(Stack stack)
    {
        int quantity = 0;
        for (int i = 0; i < stacks.Length; i++)
        {
            if (stacks[i].item == stack.item) quantity += stacks[i].quantity;        //get quantity in inventory
        }
        return quantity;
    }
}
