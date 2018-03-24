using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public StackDisplay stackDisplay;

    public int slotIndex;

    public void UpdateSlot(Stack stack)
    {
        Debug.Log(stack.quantity + " " + stack.item.name + " added in slot " + slotIndex);
        Debug.Log(stackDisplay.icon);

        stackDisplay.icon.sprite = stack.item.icon;
        stackDisplay.quantity.text = stack.quantity.ToString();
    }

    public void ClearSlot()
    {
        stackDisplay.icon.sprite = null;
        stackDisplay.quantity.text = null;
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
