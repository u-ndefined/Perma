﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hightlight : MonoBehaviour {
    private Ray ray;
    private RaycastHit hit;
    private Camera cam;
    private InventoryManager inventory;
    public Transform HexHightlighter;

	private void Start()
	{
        cam = Camera.main;
        inventory = InventoryManager.Instance;
	}


	private void FixedUpdate()
	{
        HexHightlighter.gameObject.SetActive(false);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Stack selectedStack = inventory.stacks[inventory.selectedSlotID];
            if (selectedStack.empty || selectedStack.item.itemType != ItemType.SEED)
        {
            return;
        }

        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 250, LayerMask.GetMask("Clickable")))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if(interactable is HexCell)
                {
                    if(((HexCell)interactable).isActive && ((HexCell)interactable).plant == null)
                    {
                        HexHightlighter.gameObject.SetActive(true);
                        Vector3 pos = new Vector3(hit.transform.position.x, 0.01f, hit.transform.position.z);
                        HexHightlighter.position = pos;
                    }
                   
                }
            }

        }
	}
}
