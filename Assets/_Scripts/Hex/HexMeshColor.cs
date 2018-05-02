using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HexMeshColor : MonoBehaviour {
    private HexCell hexCell;
    private bool isActive;
    public Material defaultMaterial;
    public Material desctivateHex;
    private GameObject hexMesh;
	// Update is called once per frame
	private void Start()
	{
        hexMesh = transform.GetChild(0).gameObject;
        hexCell = GetComponent<HexCell>();
        isActive = hexCell.isActive;
        ChangeMaterial();
	}
	void Update () 
    {
        if(isActive != hexCell.isActive)
        {
            isActive = hexCell.isActive;
            ChangeMaterial();
        }

	}
    private void ChangeMaterial()
    {
        if(isActive) hexMesh.GetComponent<MeshRenderer>().material = defaultMaterial;
        else hexMesh.GetComponent<MeshRenderer>().material = desctivateHex;

    }
}
