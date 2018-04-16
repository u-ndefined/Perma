using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
    private int actualGrowStep = 0;
    public bool harvestable = false;
    public bool wilted = false;
    public Seed seed;
    public Transform plantObject;
    public Material wiltedMaterial;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private CapsuleCollider plantCollider;

    private void Start()
    {
        meshFilter = plantObject.GetComponent<MeshFilter>();
        meshRenderer = plantObject.GetComponent<MeshRenderer>();
        plantCollider = plantObject.GetComponent<CapsuleCollider>();
    }

    private void SetPlant(int n)
    {
        plantObject.transform.localPosition = seed.growthSteps[n].transform.position;
        plantObject.transform.localScale = seed.growthSteps[n].transform.lossyScale;
        plantObject.transform.rotation = seed.growthSteps[n].transform.rotation;
        meshFilter.mesh = seed.growthSteps[n].GetComponent<MeshFilter>().sharedMesh;
        meshRenderer.material = seed.growthSteps[n].GetComponent<MeshRenderer>().sharedMaterial;
        plantCollider.enabled = true;
        plantCollider.radius = seed.growthSteps[n].GetComponent<CapsuleCollider>().radius;
        plantCollider.height = seed.growthSteps[n].GetComponent<CapsuleCollider>().height;
        plantCollider.center = seed.growthSteps[n].GetComponent<CapsuleCollider>().center;
    }

    public void Grow()
    {
        if(!harvestable)
        {
            actualGrowStep++;
            SetPlant(actualGrowStep);
            Debug.Log("Grow");

            if(actualGrowStep == seed.growthSteps.Length - 1)
            {
                harvestable = true;
                Debug.Log("Harvestable !");
            }
        }

    }

    public void AddSeed(Seed newSeed)
    {
        seed = newSeed;
        SetPlant(actualGrowStep);
        Debug.Log(newSeed.name + " added");
    }

    public void ResetPlant()
    {
        seed = null;
        wilted = false;
        actualGrowStep = 0;
        meshFilter.mesh = null;
        meshRenderer.material = null;
        plantCollider.enabled = false;
        /*
        plantCollider.radius = 0;
        plantCollider.height = 0;
        plantCollider.center = Vector3.zero;
        */
        harvestable = false;
    }

    public bool CanGrow()
    {
        if (seed == null) return false;
        if (wilted) return false;
        if (actualGrowStep >= seed.growthSteps.Length - 1) return false;

        return true;
    }

    public void Wilt()
    {
        meshRenderer.material = wiltedMaterial;
        Debug.Log("Wilt");
    }
}
