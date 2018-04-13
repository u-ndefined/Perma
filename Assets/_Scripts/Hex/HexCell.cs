﻿using System.Collections;
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
    public HexGrid hexGrid;

    private Plant plant;


	private void Start()
	{
        TimeManager.Instance.OnNewDayEvent += UpdateHexState;
        TimeManager.Instance.OnNewDayLateEvent += UpdatePlantState;

        plant = transform.GetChild(0).GetComponent<Plant>();

        baseHexData = hexData;

        //Debug.Log(coordinates.X + " ; " + coordinates.Z + " = " + HexCoordinates.CoordinatesToGrid(coordinates.X, coordinates.Z));
	}

	public override void Interact()
	{
        base.Interact();

        Stack stackUsed = InventoryManager.Instance.stackUsed;      //get stack used

        if(stackUsed != null)
        {
            if(stackUsed.item.itemType == ItemType.SEED && hexState == HexState.EXPOSED)    //if it's a seed and hex is exposed
            {
                plant.AddSeed((Seed)stackUsed.item);
                InventoryManager.Instance.Remove(stackUsed, 1);         //plant the seed and remove it from inventory
            }
        }
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
        if (plant.CanGrow())
        {
            hexData += plant.seed.hexEffect;                //impact its own cell
            ImpactAdjacentHexCells(plant.seed.hexEffect);   //impact adjacent cells
        }
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

     


}
