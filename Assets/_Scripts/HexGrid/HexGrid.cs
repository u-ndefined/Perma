using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class HexGrid : MonoBehaviour {
    public int width;
    public int height;
    public HexCell[,] cells;

    public Material[] debugMaterials;
    private bool meshesDisplayed = true;

	private void Start()
	{
        debugMaterials = new Material[6];

        //store all cells in the grid (bof bof)
        cells = new HexCell[height, width];
        int x = 0;
        int y = 0;
        foreach(Transform raw in transform)
        {
            foreach(Transform cell in raw)
            {
                cells[x, y] = cell.GetComponent<HexCell>();
                x++;
            }
            x = 0;
            y++;
        }

        meshesDisplayed = true;
        ToggleHexMeshes();
	}


	public HexCell SafeHexCell(int x, int y)
    {
        if(x < 0 || y < 0 || x >= width || y >= height)
        {
            return null;
        }

        return cells[x, y];
    }

    public void ToggleHexMeshes()
    {
        meshesDisplayed = !meshesDisplayed;
        int x = 0;
        int y = 0;
        foreach (Transform raw in transform)
        {
            foreach (Transform cell in raw)
            {
                cell.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = meshesDisplayed;
                x++;
            }
            x = 0;
            y++;
        }

    }

}

