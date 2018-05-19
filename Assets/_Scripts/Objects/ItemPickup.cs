using UnityEngine;

public class ItemPickup : Interactable {

    public Stack stack;

	public override void Interact()
	{
        base.Interact();
        base.DoAction(GameData.Animation.PickUp, 3, new Clock(1, 1, 1));
        onActionDoneEvent += Pickup;
	}

    private void Pickup()
    {
        onActionDoneEvent -= Pickup;
        PlayerControler.Instance.RemoveFocus();
        Stack remaining = InventoryManager.Instance.Add(stack);
        if(remaining.empty)
        {
            Destroy(gameObject);
        }
    }
}
