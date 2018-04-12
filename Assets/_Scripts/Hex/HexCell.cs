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

[System.Serializable]
public struct HexData
{
    public int light;
    public int humidity;
    public int energy;

    public HexData(int _light, int _humidity, int _energy)
    {
        light = _light;
        humidity = _humidity;
        energy = _energy;
    }

    public bool IsPositive()
    {
        if(light < 0)
        {
            return false;
        }
        if (humidity < 0)
        {
            return false;
        }
        if (energy < 0)
        {
            return false;
        }
        return true;
    }

    public static HexData operator+ (HexData a, HexData b)
    {
        HexData result = new HexData();
        result.light = a.light + b.light;
        result.humidity = a.humidity + b.humidity;
        result.energy = a.energy + b.energy;
        return result;
    }

    public static HexData operator- (HexData a, HexData b)
    {
        HexData result = new HexData();
        result.light = a.light - b.light;
        result.humidity = a.humidity - b.humidity;
        result.energy = a.energy - b.energy;
        return result;
    }

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
        

        Vector2 pos = HexCoordinates.CoordinatesToGrid(coordinates.X, coordinates.Z);

        int x = (int) pos.x;
        int y = (int) pos.y;


        ImpactAdjacentHexCell(hexGrid.SafeHexCell(x + 1, y    ), impactData);
        ImpactAdjacentHexCell(hexGrid.SafeHexCell(x - 1, y - 1), impactData);
        ImpactAdjacentHexCell(hexGrid.SafeHexCell(x    , y + 1), impactData);
        ImpactAdjacentHexCell(hexGrid.SafeHexCell(x    , y - 1), impactData);
        ImpactAdjacentHexCell(hexGrid.SafeHexCell(x - 1, y    ), impactData);
        ImpactAdjacentHexCell(hexGrid.SafeHexCell(x - 1, y + 1), impactData);


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
            hexData += plant.seed.hexEffect;
            ImpactAdjacentHexCells(plant.seed.hexEffect);
        }
	}

    private void UpdatePlantState()
    {
        if (plant.CanGrow())
        {
            if(hexData.IsPositive())
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
