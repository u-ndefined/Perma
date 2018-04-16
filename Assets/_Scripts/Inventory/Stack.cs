using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stack
{
    public Item item = null;
    public int quantity = 0;


    public Stack(Stack other)
    {
        item = other.item;
        quantity = other.quantity;
        //maxQuantity = other.maxQuantity;
    }
    
}
