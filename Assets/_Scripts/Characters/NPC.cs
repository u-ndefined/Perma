using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable 
{
    private List<Stack> inventory = new List<Stack>();
    private InventoryManager playerInventory;
    private Actor actor;

    [Header("Quest")]
    public bool quest = false;
    public Stack need;
    public Stack reward;
    private bool done = false;



	// Use this for initialization
	void Start () 
    {
        playerInventory = InventoryManager.Instance;
        actor = GetComponent<Actor>();
	}

    public override void Interact()
    {
        //base.Interact();
        if(!quest)
        {
            DialogueManager.Instance.ActorSay(actor, "Quest_start");
            quest = true;
        }
        else
        {
            if(done)
            {
                DialogueManager.Instance.ActorSay(actor, "Quest_done");
            }
            else 
            {
                int r = Mathf.FloorToInt(Random.Range(0, 3)) + 1;
                DialogueManager.Instance.ActorSay(actor, "Quest_inProgress" + r);
            }
        }

    }

    public override void UseObjectOn(Stack stackUsedOn)
    {
        base.Interact();
        switch (stackUsedOn.item.itemType)
        {
            case ItemType.SHOVEL:
                Debug.Log("Pourquoi tu me tends cette pelle?");
                break;
            default:
                stackUsedOn.quantity = 1;
                playerInventory.RemoveAtIndex(playerInventory.selectedSlotID, 1);
                Give(stackUsedOn);
                break;

        }
    }

    public void Give(Stack stack)
    {
        
        bool added = false;
        for (int i = 0; i < inventory.Count; i++)
        {
            if(stack.item == inventory[i].item)
            {
                Stack tempStack = stack;
                tempStack.quantity += inventory[i].quantity;
                inventory[i] = tempStack;
                added = true;
            }
        }
        if (!added) inventory.Add(stack);

        if(QuestEnds()) DialogueManager.Instance.ActorSay(actor, "Quest_done");
        else DialogueManager.Instance.ActorSay(actor, "NPC_receiveObject");
    }

    private bool QuestEnds()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (need.item == inventory[i].item)
            {
                if(need.quantity <= inventory[i].quantity)
                {
                    done = true;
                    return true;
                }
            }
        }
        return false;
    }
}
