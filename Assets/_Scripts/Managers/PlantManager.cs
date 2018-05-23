using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : ISingleton<PlantManager> {

    protected PlantManager(){}

    public Seed beans, carrot, carnation;

    public Seed GetSeed(HexColor color)
    {
        switch(color)
        {
            case HexColor.Carnation:
                return carnation;
            case HexColor.beans:
                return beans;
            case HexColor.carrot:
                return carrot;
            default:
                return null;
        }
    }
}
