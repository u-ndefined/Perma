using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

	public override void Interact()
	{
        base.Interact();

        Pickup();
	}

    private void Pickup()
    {
        PlayerControler.Instance.RemoveFocus();
        if(InventoryManager.Instance.Add(item))
        {
            Destroy(gameObject);
        }
    }
}
