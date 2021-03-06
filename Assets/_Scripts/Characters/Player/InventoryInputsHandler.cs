﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryInputsHandler : MonoBehaviour
{
    private TimeManager timeManager;

    private PointerEventData pointer;

    private InventorySlot startingSlot;
    private InventorySlot endingSlot;

    public StackDisplay mouseFollower;

    public bool dragging = false;
    private bool dragSet = false;

    private InventoryManager inventory;

    private Stack stackDragged;

    private List<RaycastResult> hitObjects = new List<RaycastResult>();

    private Vector2 startingPoint;

    private void Start()
    {
        inventory = InventoryManager.Instance;
        timeManager = TimeManager.Instance;
    }

    void Update()
    {
        if (timeManager.gameIsPaused || DialogueManager.Instance.isActive) return;

        if (Input.GetMouseButtonDown(0)) //when left clic
        {
            startingSlot = GetSlotUnderMouse();             //get slot under mouse

            if (startingSlot != null && !dragging && !inventory.stacks[startingSlot.slotIndex].empty)      //if begin drag
            {
                SoundManager.Instance.PlaySound("UI/InventoryIcon");

                startingPoint = Input.mousePosition;

                dragging = true;
            }
        }

        if (dragging)   //when dragging
        {
            if (!dragSet && !inventory.stacks[startingSlot.slotIndex].empty && MouseMoved())  //prevent drag empty object
            {
                stackDragged = inventory.stacks[startingSlot.slotIndex]; //copy stack before removing it

                if (Input.GetButton("Modifier1"))   //if modifier 1 get half
                {
                    Debug.Log("half");
                    stackDragged.quantity = Mathf.CeilToInt((float)stackDragged.quantity / 2);
                }
                if (Input.GetButton("Modifier2"))   //if modifier 2 get 1
                {
                    Debug.Log("one");
                    stackDragged.quantity = 1;
                }

                mouseFollower.SetDisplay(stackDragged.item.icon, stackDragged.quantity.ToString());

                inventory.RemoveAtIndex(startingSlot.slotIndex, stackDragged.quantity); //remove quantity in starting slot

                dragSet = true;
            }
            else
            {
                mouseFollower.transform.position = Input.mousePosition; //update mouseFollower position
            }
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
                        inventory.SelectSlot(startingSlot.slotIndex);  //select slot 
                        inventory.AddAtIndex(endingSlot.slotIndex, stackDragged);    //add dragged stack

                    }
                    else
                    {
                        SoundManager.Instance.PlaySound("UI/InventoryIcon");

                        stackDragged = inventory.AddAtIndex(endingSlot.slotIndex, stackDragged);                        //soft add at ending

                        if (!stackDragged.empty) stackDragged = inventory.Replace(endingSlot.slotIndex, stackDragged);      //if not empty replace

                        stackDragged = inventory.AddAtIndex(startingSlot.slotIndex, stackDragged);                         //soft add at starting

                        if (!stackDragged.empty) stackDragged = inventory.Replace(startingSlot.slotIndex, stackDragged);    //if not empty replace

                        if (!inventory.Add(stackDragged).empty) inventory.DropItem(stackDragged);                           //add anywere or drop
                    }

                }

                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    bool done = false;

                    if (Physics.Raycast(ray, out hit, 1000))                         //if interactable follow it
                    {
                        NPC npc = hit.collider.GetComponent<NPC>();
                        if(npc != null)
                        {
                            stackDragged.quantity -= 1;
                            if(stackDragged.quantity > 0)
                            {
                                inventory.AddAtIndex(startingSlot.slotIndex, stackDragged);    //add dragged stack
                            }

                            stackDragged.quantity = 1;
                            npc.Give(stackDragged);
                            stackDragged.empty = true;

                            done = true;
                        }
                    }

                    Debug.Log("create object"); //if there is no ending slot create stack on ground
                    if(!done && !stackDragged.empty) inventory.DropItem(stackDragged);
                }
            }

            else
            {
                if (endingSlot != null)
                {
                    inventory.SelectSlot(endingSlot.slotIndex);  //select slot 
                }
            }


            mouseFollower.Reset();

            stackDragged.Clear();

            dragging = false;

            dragSet = false;

            startingPoint = Vector2.zero;
        }
    }

    private InventorySlot GetSlotUnderMouse()
    {
        pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        foreach (RaycastResult hitObject in hitObjects)
        {
            InventorySlot slotUnderMouse = hitObject.gameObject.GetComponent<InventorySlot>();
            if (slotUnderMouse != null)
            {
                return slotUnderMouse;
            }
        }

        return null;
    }

    private bool MouseMoved()
    {
        Vector2 mousePosition = Input.mousePosition;
        return !startingPoint.Equals(mousePosition);
    }

}