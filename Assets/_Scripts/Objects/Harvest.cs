using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : Interactable {

    public override void Interact()
    {
        base.Interact();

        Plant plant = GetComponentInParent<Plant>();

        if(plant.wilted)
        {
            Debug.Log("Looks like it didn't enjoy being here.");
        }
        else if(!plant.harvestable)
        {
            Debug.Log("Give it more time.");
        }
        else
        {
            Debug.Log("You can harvest it !");
            Stack[] harvestContent = plant.seed.harvestContent;
            for (int i = 0; i < harvestContent.Length; i++)
            {
                if (InventoryManager.Instance.Add(harvestContent[i]))
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
}
