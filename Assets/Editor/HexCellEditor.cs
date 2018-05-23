using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(HexCell))]
public class HexCellEditor : Editor
{
    private HexColor color = HexColor.none;
    HexCell hexCell;


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        hexCell = (HexCell)target;

        if(color != hexCell.color)
        {
            color = hexCell.color;
            ChangeMaterial();

        }

    }
    private void ChangeMaterial()
    {

        foreach(Transform child in hexCell.transform)
        {
            if(child.name.Equals("HexMesh"))
            {
                child.GetComponent<MeshRenderer>().material = ColorConverter(color);
            }
        }
    }

    public Material ColorConverter(HexColor col)
    {
        switch (col)
        {
            case HexColor.none:
                return Resources.Load("Materials/White", typeof(Material)) as Material;
            case HexColor.red:
                return Resources.Load("Materials/Red", typeof(Material)) as Material;
            case HexColor.Carnation:
                return Resources.Load("Materials/Green", typeof(Material)) as Material;
            case HexColor.blue:
                return Resources.Load("Materials/Blue", typeof(Material)) as Material;
            case HexColor.carrot:
                return Resources.Load("Materials/Cyan", typeof(Material)) as Material;
            case HexColor.beans:
                return Resources.Load("Materials/Black", typeof(Material)) as Material;
            default:
                return Resources.Load("Materials/White", typeof(Material)) as Material;
            
        }
    }


}
