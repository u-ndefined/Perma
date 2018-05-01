using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowStep : MonoBehaviour
{
    private Material[] materials;
    private Material wiltedMaterial;
    private Renderer[] renderers;

    private void Awake()
    {
        renderers = new Renderer[transform.childCount];
        materials = new Material[transform.childCount];

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i] = transform.GetChild(i).GetComponent<Renderer>();
            materials[i] = renderers[i].material;
        }

        wiltedMaterial = GetComponentInParent<Plant>().wiltedMaterial;

    }

    public void ResetGrowStep()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = materials[i];
        }
    }

    public void Wilt()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material =wiltedMaterial;
        }
    }
}
