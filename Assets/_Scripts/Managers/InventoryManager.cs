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

    public Stack stackUsed = null;

    public Stack tempStack;

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
        stackUsed = null;

        selectedSlotID = slotID;

        if (onSelectorChangedEvent != null)         //update selector
        {
            onSelectorChangedEvent.Invoke();
        }
    }

    public void UseSlot()
    {
        if(stacks[selectedSlotID] != null)
        {
            Debug.Log("Use slot " + selectedSlotID);
            stackUsed = stacks[selectedSlotID];
            //stackUsed.item.Use();
        }
       
    }

    public void DropItem(Stack dropStack)
    {
        Transform player = PlayerControler.Instance.transform;
        Vector3 dropPosition = player.position + player.forward + Vector3.up;
        GameObject dropObject = Instantiate(dropStack.item.objectOnGround, dropPosition, player.rotation);
        dropObject.GetComponent<ItemPickup>().stack.quantity = dropStack.quantity;
    }

    #region Add_and_Remove_at_index
    public void AddAtIndex(int index, Stack newStack)
    {
        if(stacks[index] == null)
        {
            stacks[index] = newStack;

            if (onItemChangedEvent != null)         //updateUI
            {
                onItemChangedEvent.Invoke();
            }
        }
        else if (stacks[index].item == newStack.item)
        {
            int remainingQuantity = stacks[index].maxQuantity - stacks[index].quantity;
            if(remainingQuantity >= newStack.quantity)          //if enought place
            {
                stacks[index].quantity += newStack.quantity;    //add stack

                if (onItemChangedEvent != null)         //updateUI
                {
                    onItemChangedEvent.Invoke();
                }
            }
            else if (remainingQuantity > 0)                     //if there is place but not enought
            {
                stacks[index].quantity += remainingQuantity;
                newStack.quantity -= remainingQuantity;
                Add(newStack);                                  //add remaining quantity to Add on empty slot
            }
            else
            {
                Add(newStack);                                  //if no place, add stack to empty slot
            }
        }
        else                                                //if it's not the same object swap them together
        {
            Stack itemToSwap = new Stack(stacks[index]);
            stacks[index] = newStack;
            Add(itemToSwap);
        }
        
    }

    public void RemoveAtIndex(int index, int removedQuantity)
    {
        
        if (stacks[index].quantity > removedQuantity) //if enought quantity remove quantity
        {
            stacks[index].quantity -= removedQuantity;
        }
        else if (stacks[index].quantity == removedQuantity)  //if exact quantity remove stack
        {
            stacks[index] = null;
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

    public bool Add(Stack newStack)
    {
        for (int i = 0; i < stacks.Length; i++)
        {

            if (stacks[i] != null && stacks[i].item == newStack.item)               //if item already in inventory
            {
                int remainingQuantity = stacks[i].maxQuantity - stacks[i].quantity; // get remaining quantity

                if (remainingQuantity >= newStack.quantity)     //if enought place
                {
                    stacks[i].quantity += newStack.quantity;        //add stack

                    if (onItemChangedEvent != null)         //updateUI
                    {
                        onItemChangedEvent.Invoke();
                    }

                    Debug.Log( newStack.quantity + " " + newStack.item.name + " added to an existing stack");

                    return true;
                }

                if(remainingQuantity > 0)                   //if there is place but not enought
                {
                    stacks[i].quantity += remainingQuantity;    //add remaining quantity to the stack
                    newStack.quantity -= remainingQuantity; //and substract this quantity to the stack to add

                    Debug.Log(remainingQuantity + " " + newStack.item.name + " added to an existing stack");
                }
            }
        }

        //if this part is reach this means there is remaining quantity in the stack to add

        for (int i = 0; i < stacks.Length; i++)     //search for an empty slot
        {
            if(stacks[i] == null)                   //if empty add new stack
            {
                stacks[i] = newStack;

                Debug.Log(newStack.quantity + " " + newStack.item.name + " added to a new slot");

                if (onItemChangedEvent != null)         //updateUI
                {
                    onItemChangedEvent.Invoke();
                }

                return true;
            }
        }


        Debug.Log("Inventory is full");      //if inventory full return false

        if (onItemChangedEvent != null)         //updateUI
        {
            onItemChangedEvent.Invoke();
        }


        return false;

    }

    public void Remove(Stack removedStack, int removedQuantity)
    {

        for (int i = stacks.Length - 1; i >= 0; i--)     //browse backward to remove safely (old)
        {

            if (stacks[i] != null && stacks[i].item == removedStack.item) //find the stack to remove
            {
                if (stacks[i].quantity > removedQuantity) //if enought quantity remove quantity
                {
                    stacks[i].quantity -= removedQuantity;
                    removedQuantity = 0;
                    break;
                }
                else if (stacks[i].quantity == removedQuantity)  //if exact quantity remove stack
                {
                    stacks[i] = null;
                    removedQuantity = 0;
                    break;
                }
                else
                {
                    removedQuantity -= stacks[i].quantity;        //if not enought quantity
                    stacks[i] = null;                          //remove stack and its quantity and continue
                    Debug.Log(removedQuantity + " remaining " + removedStack.item.name + " to remove");
                }
            }
        }

        if(removedQuantity > 0)
        {
            Debug.Log("!!! " + removedQuantity + " remaining " + removedStack.item.name + " to remove");
        }


        if (onItemChangedEvent != null)         //updateUI
        {
            onItemChangedEvent.Invoke();
        }

    }
    #endregion
}
