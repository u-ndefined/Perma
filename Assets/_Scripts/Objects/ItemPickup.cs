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
        stack = InventoryManager.Instance.Add(stack);
        SoundManager.Instance.PlaySound("PlayerAction/Pickup2");
        if(stack.empty)
        {
            Destroy(gameObject);
        }
    }
}
