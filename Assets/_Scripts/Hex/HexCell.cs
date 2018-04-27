﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HexType 
{
    FOREST,
    FOREST_BORDER,
    MEADOW
}

public enum HexState
{
    COVERED,
    EXPOSED,
    DUG
}

public class HexCell : Interactable {
    private HexData baseHexData;
    public HexCoordinates coordinates;
    public bool isActive = true;
    public HexType type;
    public HexState hexState;
    public HexData hexData;
    public HexData hexDataMax;
    [HideInInspector]
    public HexGrid hexGrid;
    [HideInInspector]
    public Plant plant;

    [Header("Day effect")]
    public HexData dayEffectToAdd;
    public HexData dayEffectToSet;
    public bool setLight, setHumidity, setEnergy;


	private void Start()
    {
        TimeManager.Instance.OnNewDayEvent += UpdateHexState;
        TimeManager.Instance.OnNewDayLateEvent += UpdatePlantState;

        plant = GetComponent<Plant>();

        baseHexData = hexData;
    }

    public override void Interact()
	{
        if(!isActive)   //there is better solutions
        {
            return;
        }

        InventoryManager inventory = InventoryManager.Instance;

        Stack stackUsed = inventory.stackUsed;      //get stack used

        if(stackUsed != null)
        {
            if(stackUsed.item.itemType == ItemType.SEED && hexState == HexState.EXPOSED && plant.seed == null)    //if it's a seed and hex is exposed and there is no seed in this hex
            {
                Debug.Log(stackUsed.item.name);
                if(inventory.stacks[inventory.selectedSlotID].item == stackUsed.item )
                {
                    SoundManager.Instance.PlaySound("arrose" + PlayerControler.Instance.transform.GetInstanceID());
                    plant.AddSeed((Seed)stackUsed.item);
                    inventory.RemoveAtIndex(inventory.selectedSlotID,1);         //plant the seed and remove it from inventory
                }
               
            }
            if (stackUsed.item.itemType == ItemType.SHOVEL && plant.seed != null)    //if it's a seed and hex is exposed
            {
                Debug.Log("plouf");
                plant.ResetPlant();



            }
        }

        base.Interact();
	}

    private void ChangeColor()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    private void ImpactAdjacentHexCells(HexData impactData)
    {

        Vector2[] adjacentPos = HexCoordinates.GetAdjacents(coordinates.X, coordinates.Z);

        int x;
        int y;


        for (int i = 0; i < adjacentPos.Length; i++)
        {
            x = (int)adjacentPos[i].x;
            y = (int)adjacentPos[i].y;
            ImpactAdjacentHexCell(hexGrid.SafeHexCell(x , y), impactData);
        }

    }

    private void ImpactAdjacentHexCell(HexCell cell, HexData impactData)
    {
        if(cell == null)
        {
            return;
        }

        cell.hexData += impactData;
        //cell.ChangeColor();
    }

	private void UpdateHexState()      // call first on next day
    {
        if(plant.seed == null)
        {
            DayEffect();
        }

        if (plant.CanGrow())
        {
            hexData += plant.seed.hexEffect;                //impact its own cell
            ImpactAdjacentHexCells(plant.seed.hexEffect);   //impact adjacent cells
        }

        hexData = ClampHexData();   //clamp hexData with maxHexData
	}

    private void UpdatePlantState()
    {
        if (plant.CanGrow())
        {
            if((hexData - plant.seed.plantNeeds).IsPositive())
            {
                plant.Grow();
            }
            else
            {
                plant.Wilt();
            }
        }
    }

    private void ResetHexCell()
    {
        hexData = baseHexData;  //reset hex data with baseHexData
        plant.ResetPlant();
    }

    private void DayEffect()
    {
        if(setLight)
        {
            hexData.light = dayEffectToSet.light;
        }
        if (setEnergy)
        {
            hexData.energy = dayEffectToSet.energy;
        }
        if (setHumidity)
        {
            hexData.humidity = dayEffectToSet.humidity;
        }

        hexData += dayEffectToAdd;
    }

    private HexData ClampHexData()
    {
        HexData result = new HexData();

        result.light = hexData.light > hexDataMax.light ? hexDataMax.light : hexData.light;
        result.humidity = hexData.humidity > hexDataMax.humidity ? hexDataMax.humidity : hexData.humidity;
        result.energy = hexData.energy > hexDataMax.energy ? hexDataMax.energy : hexData.energy;

        return result;
    }
}