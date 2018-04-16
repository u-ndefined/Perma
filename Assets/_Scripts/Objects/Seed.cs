using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New seed", menuName = "Items/Seed")]
[System.Serializable]
public class Seed : Item {

    //public Mesh[] growthSteps;

    public GameObject[] growthSteps;

    public HexData hexEffect;
    public HexData plantNeeds;

    public Stack[] harvestContent;

    public override void Use()
    {
        base.Use();
    }

}
