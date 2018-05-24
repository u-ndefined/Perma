using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IPooledObject {
    private int actualGrowthStep = 0;
    private GrowStep[] growthSteps;
    public bool harvestable = false;
    public bool wilted = false;
    public Seed seed;
    //public GameObject plantObject;
    public Material wiltedMaterial;


    private GameData.Prefabs plantType;




    public void OnObjectSpawn()
    {
        ResetPlant();
    }

    public void OnGoBackToPool()
    {
        ResetPlant();
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        growthSteps = new GrowStep[transform.childCount];

        for (int i = 0; i < growthSteps.Length; i++)
        {
            growthSteps[i] = transform.GetChild(i).GetComponent<GrowStep>();
        }

    }

    private void NextStep()
    {
            
            growthSteps[actualGrowthStep].gameObject.SetActive(false);
            actualGrowthStep++;
            growthSteps[actualGrowthStep].gameObject.SetActive(true);

    }


    public void Grow()
    {
        if(!harvestable)
        {
            NextStep();

            if(actualGrowthStep == growthSteps.Length - 1)
            {
                harvestable = true;
            }
        }

    }


    public void ResetPlant()
    {
        wilted = false;
        harvestable = false;
        actualGrowthStep = 0;
        for (int i = 0; i < growthSteps.Length; i++)
        {
            growthSteps[i].ResetGrowStep();
            if (i == 0) growthSteps[i].gameObject.SetActive(true);
            else growthSteps[i].gameObject.SetActive(false);
        }
    }

    public bool CanGrow()
    {
        if (wilted) return false;

        //if (actualGrowthStep >= growthSteps.Length - 1) return false;

        return true;
    }

    public void Wilt()
    {
        wilted = true;
        growthSteps[actualGrowthStep].Wilt();
    }

    public void DestroyPlant()
    {
        HexCell cell = GetComponentInParent<HexCell>();
        if (cell) cell.DestroyPlant();

    }

   
}
