using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable 
{
    private List<Stack> inventory = new List<Stack>();
    private InventoryManager playerInventory;
    private Actor actor;
    private Animator animatorr;

    [Header("Quest")]
    public bool quest = false;
    public Stack need;
    public Stack reward;
    private bool done = false;



	// Use this for initialization
    protected override void Start () 
    {
        base.Start();
        playerInventory = InventoryManager.Instance;
        actor = GetComponent<Actor>();
        animatorr = GetComponent<Animator>();
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
                if(playerInventory.GetQuantity(need) >= need.quantity)
                {
                    playerInventory.Remove(need);
                    Give(need);
                    UpdateQuestStatus();
                }
                else
                {
                    int r = Mathf.FloorToInt(Random.Range(0, 3)) + 1;
                    DialogueManager.Instance.ActorSay(actor, "Quest_inProgress" + r);
                }

            }
        }
        animatorr.SetTrigger("NPCSpeak");
        DoAction(PlayerManager.Instance.GetAnim(GameData.Animation.Speak));
    }

	public override void UseObjectOn(Stack stackUsedOn)
    {
        base.Interact();
        stackUsedOn.quantity = 1;
        playerInventory.RemoveAtIndex(playerInventory.selectedSlotID, 1);
        Give(stackUsedOn);
    }

    public void Give(Stack stack)
    {
        if(stack.item.itemType == ItemType.SHOVEL)
        {
            InventoryManager.Instance.Add(stack);
            return;
        }
        
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

        if (UpdateQuestStatus()) EndQuest();
        else DialogueManager.Instance.ActorSay(actor, "NPC_receiveObject");

    }

    private bool UpdateQuestStatus()
    {
        if (done) return false;
        for (int i = 0; i < inventory.Count; i++)
        {
            Stack stack = inventory[i];
            if (need.item == stack.item)
            {
                if(need.quantity <= stack.quantity)
                {
                    stack.quantity -= need.quantity;
                    inventory[i] = stack;
                    return true;
                }
            }
        }
        return false;
    }

    private void EndQuest()
    {
        DialogueManager.Instance.ActorSay(actor, "Quest_done");
        quest = done;
        playerInventory.Add(reward);
    }
}
