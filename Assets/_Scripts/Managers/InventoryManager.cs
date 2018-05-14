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


    void Awake()
    {
        stacks = new Stack[space];

    }

	private void Start()
	{
        if (onItemChangedEvent != null)         //updateUI
        {
            onItemChangedEvent.Invoke();
        }

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
        if(stacks[selectedSlotID].empty)
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
        Vector3 dropPosition = player.position + player.forward + Vector3.up;
        GameObject dropObject = Instantiate(dropStack.item.objectOnGround, dropPosition, player.rotation);
        dropObject.GetComponent<ItemPickup>().stack.quantity = dropStack.quantity;
    }

    #region Add_and_Remove_at_index
    public void AddAtIndex(int index, Stack addedStack)
    {
        if (index < 0 || index >= stacks.Length) return;    //if wrong index return

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

        if (!addedStack.empty) addedStack = Add(addedStack);
        if (!addedStack.empty) DropItem(addedStack);
     
    }

    public void SwapStack(int a, int b)
    {
        Stack tempStack = stacks[a];
        stacks[a] = stacks[b];
        stacks[b] = tempStack;
    }


    public void RemoveAtIndex(int index, int removedQuantity)
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
            Debug.Log("!!! " + removedQuantity + " remaining " + stacks[index].item.name + " to remove");
        }

        if (onItemChangedEvent != null)         //updateUI
        {
            onItemChangedEvent.Invoke();
        }
    }

    #endregion


    #region Add_and_Remove

    public Stack Add(Stack addedStack)
    {
        SoundManager.Instance.PlaySound("PlayerAction/Pickup2");

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
        int quantity = 0;
        for(int i = 0; i < stacks.Length; i++)
        {
            if (stacks[i].item == removedStack.item) quantity += stacks[i].quantity;        //get quantity in inventory
        }
        if(quantity > removedStack.quantity)                                                //if enought
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
}
