using UnityEngine;

public class ItemPickup : Interactable {

    public Stack stack;

	public override void Interact()
	{
        base.Interact();

        Pickup();
	}

    private void Pickup()
    {
        PlayerControler.Instance.RemoveFocus();
        Stack remaining = InventoryManager.Instance.Add(stack);
        if(remaining.empty)
        {
            Destroy(gameObject);
        }
    }
}
