﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : Interactable
{

    Plant plant;

    private void Start()
    {
        plant = GetComponentInParent<Plant>();
    }

    public override void Interact()
    {



        if (plant.wilted)
        {
            DialogueManager.Instance.PlayerSay("Wilt");
        }
        else if (!plant.harvestable)
        {
            DialogueManager.Instance.PlayerSay("Growing");
        }
        else
        {
            Debug.Log("You can harvest it !");
            Stack[] harvestContent = plant.seed.harvestContent;
            for (int i = 0; i < harvestContent.Length; i++)
            {
                Stack content = new Stack(harvestContent[i]);
                if (!InventoryManager.Instance.Add(content))
                {
                    InventoryManager.Instance.DropItem(harvestContent[i]);
                }
                plant.DestroyPlant();
            }
        }

        base.Interact();
    }

    public override void UseObjectOn(Stack stackUsedOn)
    {
        if (stackUsedOn.item.itemType == ItemType.SHOVEL)
        {
            plant.DestroyPlant();
            SoundManager.Instance.PlaySound("PlayerAction/Dig");
        }

        base.UseObjectOn(stackUsedOn);
    }
}
