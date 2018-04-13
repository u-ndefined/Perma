using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
    private int actualGrowStep = 0;
    private bool wilted = false;
    public Seed seed;
    private MeshFilter meshFilter;

    private void Start()
    {
        
        meshFilter = GetComponent<MeshFilter>();
    }

    public void Grow()
    {
        actualGrowStep++;
        meshFilter.mesh = seed.growthSteps[actualGrowStep];
        Debug.Log("Grow");
    }

    public void AddSeed(Seed newSeed)
    {
        seed = newSeed;
        meshFilter.mesh = seed.growthSteps[actualGrowStep];
        Debug.Log(newSeed.name + " added");
    }

    public void ResetPlant()
    {
        seed = null;
        wilted = false;
        actualGrowStep = 0;
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
        GetComponent<Renderer>().material.color = Color.yellow;   //color plant in yellow
        Debug.Log("Wilt");
    }
}
