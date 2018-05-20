using UnityEngine;

public class ItemPickup : Interactable {

    public Stack stack;

	public override void Interact()
	{
        base.Interact();
        base.DoAction(GameData.Animation.PickUp, 3, new Clock(1, 1, 1));
        //onActionDoneEvent += Pickup;
        Pickup();
	}

    private void Pickup()
    {
        //onActionDoneEvent -= Pickup;
        PlayerControler.Instance.RemoveFocus();
        stack = InventoryManager.Instance.Add(stack);
        if(stack.empty)
        {
            Destroy(gameObject);
        }
    }
}
