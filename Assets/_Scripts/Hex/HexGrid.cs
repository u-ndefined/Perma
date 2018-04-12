using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HexGrid : MonoBehaviour {
    public int width;
    public int height;
    public HexCell[,] cells;

	private void Start()
	{

        //store all cells in the grid (bof bof)
        cells = new HexCell[width, height];
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
	}


	public HexCell SafeHexCell(int x, int y)
    {


        if(x < 0 || y < 0 || x > width || y > height)
        {
            return null;
        }


        return cells[x, y];
    }
}
