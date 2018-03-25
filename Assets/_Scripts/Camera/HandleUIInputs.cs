using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class HandleUIInputs : MonoBehaviour, 
IPointerClickHandler , 
IDragHandler, 
IPointerEnterHandler, 
IPointerExitHandler, 
IBeginDragHandler,
IDropHandler,
IPointerUpHandler
{
    InventorySlot slot;

    InventoryManager inventory;

    public StackDisplay mouseFollower;

    void Start()
    {
        slot = GetComponent<InventorySlot>();
        inventory = InventoryManager.Instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        mouseFollower.gameObject.SetActive(true);
        mouseFollower.icon.sprite = slot.stackDisplay.icon.sprite;
        mouseFollower.quantity.text = slot.stackDisplay.quantity.text;
        mouseFollower.transform.position = Input.mousePosition;

        inventory.tempStack = inventory.stacks[slot.slotIndex];

        inventory.RemoveAtIndex(slot.slotIndex, Convert.ToInt32(mouseFollower.quantity.text));

    }

    public void OnDrag(PointerEventData eventData)
    {
        mouseFollower.transform.position = Input.mousePosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        inventory.AddAtIndex(slot.slotIndex, inventory.tempStack);
        inventory.tempStack = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Release");

        if(inventory.tempStack != null)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                inventory.AddAtIndex(slot.slotIndex, inventory.tempStack);
            }
            else
            {
                Debug.Log("create new item");
            }
        }

        mouseFollower.gameObject.SetActive(false);
    }

	public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clic");
        inventory.SelectSlot(slot.slotIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
    }


	void Update () 
    {
		
	}

   
}
