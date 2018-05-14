using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Fonctions utile
/// <summary>
public static class GameData
{
    #region core script

    public enum Prefabs
    {
        CARROT,
        TOMATO,
        BEANS
    }

    public enum Animation
    {
        Dig,
        Plant,
        Harvest,
        Speak,
    }

    /*
    public enum Layers
    {
        Object,         //tout les objets du décors actif (les boules)
        Player,         //les balls
        Rope,           //les link sont dans ce layer
    }

    public enum Sounds
    {
        ShockWave,
        Bump,
        Explode,
        Jump,
        Bonus,
        Swouch,
        SpiksOn,
        SpiksOff,
        Thrower,
    }


    /// <summary>
    /// retourne vrai si le layer est dans la list
    /// </summary>
    public static bool IsInList(List<Layers> listLayer, int layer)
    {
        string layerName = LayerMask.LayerToName(layer);
        for (int i = 0; i < listLayer.Count; i++)
        {
            if (listLayer[i].ToString() == layerName)
            {
                return (true);
            }
        }
        return (false);
    }
    /// <summary>
    /// retourne vrai si le layer est dans la list
    /// </summary>
    public static bool IsInList(List<Prefabs> listLayer, string tag)
    {
        for (int i = 0; i < listLayer.Count; i++)
        {
            if (listLayer[i].ToString() == tag)
            {
                return (true);
            }
        }
        return (false);
    }
    */
    #endregion
}
