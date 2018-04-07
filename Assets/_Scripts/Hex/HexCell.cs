using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HexType 
{
    Forest,
    ForestBorder,
    Meadow
}

public class HexCell : Interactable {
    public HexCoordinates coordinates;
    public bool isActive = true;
    public HexType type;
    public Seed seed;
    private int actualGrowStep = 0;

	private void Start()
	{
        TimeManager.Instance.OnNewDayEvent += UpdateState;
	}

	public override void Interact()
	{
        base.Interact();

        Stack stackUsed = InventoryManager.Instance.stackUsed;

        if(seed == null && stackUsed.item is Seed)
        {
            seed = (Seed) stackUsed.item;
            InventoryManager.Instance.Remove(stackUsed, 1);
            ChangeColor();
        }
	}

    private void ChangeColor()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

	private void UpdateState()
    {
        if(seed != null && actualGrowStep < seed.growthSteps.Length)
        {
            transform.GetChild(0).GetComponent<MeshFilter>().mesh = seed.growthSteps[actualGrowStep];
            actualGrowStep++;
        }

	}
}
