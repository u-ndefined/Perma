using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HexType 
{
    Forest,
    ForestBorder,
    Meadow
}

public class HexCell : MonoBehaviour {
    public HexCoordinates coordinates;
    public bool isActive = true;
    public HexType type;
}
