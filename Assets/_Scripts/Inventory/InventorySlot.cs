using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Image icon;

    public Sprite empty;

    private Item item;

    public int slotIndex;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        //icon.enabled = true;

        Debug.Log("Item added in slot " + slotIndex);
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = empty;
        //icon.enabled = false;
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }

    public void SelectSlot()
    {
        Debug.Log("Select this slot " + slotIndex);
        InventoryManager.Instance.SelectSlot(slotIndex);
    }
}
