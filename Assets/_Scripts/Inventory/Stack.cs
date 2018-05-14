using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Stack
{
    public Item item;
    public int quantity;
    public bool empty;



    public Stack(Item _item, int _quantity)
    {
        item = _item;
        quantity = _quantity;
        empty = quantity > 0 ? false : true;
    }

    public Stack SafeAddStack(Stack stack, bool safeMode = true)
    {
        if (stack.empty) return stack;

        if (safeMode)
        {
            if (empty) return stack;
            if (stack.item != item) return stack; //different items => return stack
        }
        else
        {
            if(empty)
            {
                item = stack.item;
                quantity = 0;
                empty = false;
            }
            else
            {
                return stack;
            }
        }


        quantity += stack.quantity;             //add quantity

        if (quantity > stack.item.maxQuantity)         //if exceed max quantity
        {
            stack.quantity = quantity - item.maxQuantity;   //set surplus on the stack
            quantity = item.maxQuantity;        //set quantity to max
            return stack;                       //return stack
        }



        return new Stack(null, 0);      //if added completly return null/empty
    }

    public Stack RemoveStack(Stack stack)
    {
        if (stack.item != item) return stack;   //different items => return stack

        quantity -= stack.quantity;             //remove quantity

        if (quantity <= 0)                        //if below 0
        {
            if (quantity == 0) stack.Clear();           //if exact count clear stack
            else stack.quantity = Mathf.Abs(quantity);   //get remaining to remove
            Clear();                               //clear
            return stack;                       //return stack
        }

        return new Stack(null, 0);      //if removed completly return null/empty
    }

    public void Clear()
    {
        item = null;
        quantity = 0;
        empty = true;
    }

    public static Stack Empty()
    {
        return new Stack(null, 0);
    }

    public string StringDebug()
    {
        if (empty) return "Empty";
        else return quantity + " " + item.name;
    }


    /*
    public Stack(Stack other)
    {
        item = other.item;
        quantity = other.quantity;
        //maxQuantity = other.maxQuantity;
    }
    */

}
