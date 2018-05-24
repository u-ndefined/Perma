using System.Collections;
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
            HexData data = plant.transform.GetComponentInParent<HexCell>().hexData;
            if(data.energy < plant.seed.plantNeeds.energy)DialogueManager.Instance.PlayerSay("WiltEnergy");
            else if (data.humidity < plant.seed.plantNeeds.humidity) DialogueManager.Instance.PlayerSay("WiltHumidity");
            else if (data.light < plant.seed.plantNeeds.light) DialogueManager.Instance.PlayerSay("WiltLight");
        }
        else if (!plant.harvestable)
        {
            DialogueManager.Instance.PlayerSay("Growing");
        }
        else
        {
            

            base.DoAction(PlayerManager.Instance.GetAnim(GameData.Animation.Harvest));
            onActionDoneEvent += HarvestPlant;

          
        }

        base.Interact();
    }

    private void HarvestPlant()
    {
        SoundManager.Instance.PlaySound("PlayerAction/Pickup2");
        Stack[] harvestContent = plant.seed.harvestContent;
        for (int i = 0; i < harvestContent.Length; i++)
        {
            Stack remaining = InventoryManager.Instance.Add(harvestContent[i]);
            if (!remaining.empty) InventoryManager.Instance.DropItem(remaining);


        }
        plant.DestroyPlant();
    }

    public override void UseObjectOn(Stack stackUsedOn)
    {
        if (stackUsedOn.item.itemType == ItemType.SHOVEL)
        {
            base.DoAction(PlayerManager.Instance.GetAnim(GameData.Animation.Dig));
            onActionDoneEvent += Dig;
        }

        base.UseObjectOn(stackUsedOn);
    }

    private void Dig()
    {
        plant.DestroyPlant();
        SoundManager.Instance.PlaySound("PlayerAction/Dig");
    }
}
