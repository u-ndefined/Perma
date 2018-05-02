using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HexGrid))]
public class HexGridEditor : Editor {
	public override void OnInspectorGUI()
	{
        DrawDefaultInspector();
        HexGrid hexGrid = (HexGrid)target;
        if(GUILayout.Button("Hide or display hex meshes"))
        {
            hexGrid.ToggleHexMeshes();
        }
	}
}
