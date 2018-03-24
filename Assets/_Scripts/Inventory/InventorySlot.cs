using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Image icon;

    [SerializeField]
    private Sprite empty;

    private Stack itemStack;

    public int slotIndex;

    public void AddStack(Stack newItem)
    {
        itemStack = newItem;

        icon.sprite = itemStack.item.icon;

        Debug.Log("Item added in slot " + slotIndex);
    }

    public void ClearSlot()
    {
        itemStack = null;
        icon.sprite = empty;
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
