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

    void Start()
    {
        stacks = new Stack[space];
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
        selectedSlotID = slotID;

        if (onSelectorChangedEvent != null)         //update selector
        {
            onSelectorChangedEvent.Invoke();
        }
    }

    public void UseSlot()
    {
        Debug.Log("Use slot " + selectedSlotID);
        //stacks[selectedSlotID].item.UseItem();
    }



    #region Add_and_Remove

    public bool Add(Stack newStack)
    {
        for (int i = 0; i < stacks.Length; i++)
        {
            Stack stack = stacks[i];

            if (stack != null && stack.item == newStack.item)               //if item already in inventory
            {
                int remainingQuantity = stack.maxQuantity - stack.quantity; // get remaining quantity

                if (remainingQuantity >= newStack.quantity)     //if enought place
                {
                    stack.quantity += newStack.quantity;        //add stack

                    if (onItemChangedEvent != null)         //updateUI
                    {
                        onItemChangedEvent.Invoke();
                    }

                    Debug.Log( newStack.quantity + " " + newStack.item.name + " added to an existing stack");

                    return true;
                }

                if(remainingQuantity > 0)                   //if there is place but not enought
                {
                    stack.quantity += remainingQuantity;    //add remaining quantity to the stack
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

    public void Remove(Stack removedStack, int removeQuantity)
    {

        for (int i = stacks.Length - 1; i >= 0; i--)     //browse backward to remove safely (old)
        {
            Stack stack = stacks[i];

            if (stack.item == removedStack.item) //find the stack to remove
            {
                if (stack.quantity > removeQuantity) //if enought quantity remove quantity
                {
                    stack.quantity -= removeQuantity;
                    removeQuantity = 0;
                    break;
                }
                else if (stack.quantity == removeQuantity)  //if exact quantity remove stack
                {
                    stack = null;
                    removeQuantity = 0;
                    break;
                }
                else
                {
                    removeQuantity -= stack.quantity;        //if not enought quantity
                    stack = null;                          //remove stack and its quantity and continue
                    Debug.Log(removeQuantity + " remaining " + removedStack.item.name + " to remove");
                }
            }
        }

        if(removeQuantity > 0)
        {
            Debug.Log("!!! " + removeQuantity + " remaining " + removedStack.item.name + " to remove");
        }


        if (onItemChangedEvent != null)         //updateUI
        {
            onItemChangedEvent.Invoke();
        }

    }
    #endregion
}
