using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New seed", menuName = "Items/Seed")]
[System.Serializable]
public class Seed : Item {

    public Mesh[] growthSteps;

    public HexData hexEffect;

    public override void Use()
    {
        base.Use();
    }

}
