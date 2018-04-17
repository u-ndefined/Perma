using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : Interactable {

    public override void Interact()
    {
        base.Interact();

        Stack stackUsed = InventoryManager.Instance.stackUsed;      //get stack used

        if(stackUsed == null)   //if no stack used == harvest
        {

        Plant plant = GetComponentInParent<Plant>();

            if (plant.wilted)
            {
                Debug.Log("Looks like it didn't enjoy being here.");
            }
            else if (!plant.harvestable)
            {
                Debug.Log("Give it more time.");
            }
            else
            {
                Debug.Log("You can harvest it !");
                Stack[] harvestContent = plant.seed.harvestContent;
                for (int i = 0; i < harvestContent.Length; i++)
                {
                    Stack content = new Stack(harvestContent[i]);
                    if (InventoryManager.Instance.Add(content))
                    {
                        plant.ResetPlant();
                    }
                    else
                    {
                        InventoryManager.Instance.DropItem(harvestContent[i]);
                    }
                }
            }
        }
        else
        {
            if(stackUsed.item.itemType == ItemType.SHOVEL)
            {
                Plant plant = GetComponentInParent<Plant>();

                plant.ResetPlant();
            }
        }

    }
}
