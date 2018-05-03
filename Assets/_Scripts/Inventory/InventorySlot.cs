using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public StackDisplay stackDisplay;

    public int slotIndex;

    public ItemDescription itemDescription;

    private bool isActive;
    private Item currentItem;

    public void UpdateSlot(Stack stack)
    {
        //Debug.Log(stack.quantity + " " + stack.item.name + " updated in slot " + slotIndex);
        isActive = true;
        currentItem = stack.item;
        stackDisplay.icon.enabled = true;
        stackDisplay.icon.sprite = stack.item.icon;
        if(stack.quantity > 1)stackDisplay.quantity.text = stack.quantity.ToString();
    }

    public void ClearSlot()
    {
        isActive = false;
        stackDisplay.icon.enabled = false;
        stackDisplay.icon.sprite = null;
        stackDisplay.quantity.text = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isActive)
            itemDescription.Show(currentItem, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemDescription.Hide();
    }

    /*
    public void UseItem()
    {
        if(itemStack != null)
        {
            itemStack.item.Use();
        }
    }

    public void SelectSlot()
    {
        Debug.Log("Select this slot " + slotIndex);
        InventoryManager.Instance.SelectSlot(slotIndex);
    }
    */
}
