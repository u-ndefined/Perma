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
        if(InventoryManager.Instance.Add(stack))
        {
            Destroy(gameObject);
        }
    }
}
