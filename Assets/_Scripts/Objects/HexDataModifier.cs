using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Frequency
{
    ADD_ONCE,
    SET_ONCE,
    ADD_EACH_DAY,
    SET_EACH_DAY
}

public class HexDataModifier : MonoBehaviour 
{
    public int range = 1;
    public HexData hexEffect;
    public Frequency frequency;

    private HexCell hexCell;

	private void Start()
	{
        hexCell = GetComponentInParent<HexCell>();

        switch(frequency)
        {
            case Frequency.ADD_ONCE:
                AddHexData();
                break;
            case Frequency.SET_ONCE:
                SetHexData();
                break;
            case Frequency.ADD_EACH_DAY:
                AddHexData();
                TimeManager.Instance.OnNewDayEvent += AddHexData;
                break;
            case Frequency.SET_EACH_DAY:
                SetHexData();
                TimeManager.Instance.OnNewDayEvent += SetHexData;
                break;
        }


	}

    private void SetHexData()
    {
        HexCell[] cellsInRange = GetHexCellsInRange();

        for (int i = 0; i < cellsInRange.Length; i++)
        {
            cellsInRange[i].hexData = hexEffect;
        }
    }

    private void AddHexData()
    {
        HexCell[] cellsInRange = GetHexCellsInRange();

        for (int i = 0; i < cellsInRange.Length; i++)
        {
            cellsInRange[i].hexData += hexEffect;
        }
    }

    private HexCell[] GetHexCellsInRange()
    {
        List<HexCell> ListOfcellsInRange = new List<HexCell>();

        Vector3 center = hexCell.transform.position;

        float radius = hexCell.hexGrid.transform.lossyScale.x * HexMetrics.innerRadius * (range + 1);

        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            HexCell newCell = hitColliders[i].GetComponent<HexCell>();

            if(newCell != null)
            {
                ListOfcellsInRange.Add(newCell);
            }
        }

        return ListOfcellsInRange.ToArray();
    }
}
