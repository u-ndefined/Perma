﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryInputsHandler : MonoBehaviour
{

    private PointerEventData pointer;

    private InventorySlot startingSlot;
    private InventorySlot endingSlot;

    public StackDisplay mouseFollower;

    private bool dragging = false;

    private InventoryManager inventory;

    private Stack stackDragged;

    private List<RaycastResult> hitObjects = new List<RaycastResult>();

    private void Start()
	{
        inventory = InventoryManager.Instance;
	}

	void Update()
    {
        if (Input.GetMouseButtonDown(0)) //when mouse pressed
        {
            startingSlot = GetSlotUnderMouse();             //get slot under mouse

            if (startingSlot != null && !dragging)      //if begin drag
            {
                if (inventory.stacks[startingSlot.slotIndex] != null)  //prevent drag empty object
                {
                    stackDragged = new Stack(inventory.stacks[startingSlot.slotIndex]); //copy stack before removing it

                    if(Input.GetButton("Modifier1"))   //if modifier 1 get half
                    {
                        Debug.Log("half");
                        stackDragged.quantity = Mathf.CeilToInt((float)stackDragged.quantity / 2);
                    }
                    if (Input.GetButton("Modifier2"))   //if modifier 2 get 1
                    {
                        Debug.Log("one");
                        stackDragged.quantity = 1;
                    }

                    mouseFollower.gameObject.SetActive(true);                                                   //set mouseFollower
                    mouseFollower.icon.sprite = startingSlot.stackDisplay.icon.sprite;
                    mouseFollower.quantity.text = stackDragged.quantity.ToString();



                    inventory.RemoveAtIndex(startingSlot.slotIndex, stackDragged.quantity); //remove quantity in starting slot


                    dragging = true;
                }
            }
        }

        if (dragging)   //when dragging
        {
            mouseFollower.transform.position = Input.mousePosition; //updte mouseFollower position
        }

        if (Input.GetMouseButtonUp(0))  //when release
        {
            if (startingSlot != null)
            {
                endingSlot = GetSlotUnderMouse();   //get slot under mouse

                if (endingSlot != null)
                {
                    if (endingSlot == startingSlot) //if it's the same than starting slot
                    {
                        Debug.Log("select slot " + startingSlot.slotIndex); 
                        inventory.SelectSlot(startingSlot.slotIndex);   //add dragged stack
                        inventory.AddAtIndex(endingSlot.slotIndex, stackDragged);   //and select it
                    }
                    else
                    {
                        Debug.Log("add to slot " + endingSlot.slotIndex);
                        inventory.AddAtIndex(endingSlot.slotIndex, stackDragged);   //else add stack to ending slot
                    }
                }
                else
                {
                    Debug.Log("create object"); //if there is no ending slot create stack on ground
                }
            }

            mouseFollower.gameObject.SetActive(false);      //unset mouseFollower
            mouseFollower.icon.sprite = null;
            mouseFollower.quantity.text = null;

            stackDragged = null;

            dragging = false;
        }
    }

    private InventorySlot GetSlotUnderMouse()
    {
        pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        foreach(RaycastResult hitObject in hitObjects)
        {
            InventorySlot slotUnderMouse = hitObject.gameObject.GetComponent<InventorySlot>();
            if(slotUnderMouse != null)
            {
                return slotUnderMouse;
            }
        }

        return null;
    }


}