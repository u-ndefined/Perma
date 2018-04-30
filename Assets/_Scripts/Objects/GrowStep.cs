using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowStep : MonoBehaviour
{
    private Material material;
    private Material wiltedMaterial;
    private new Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
        wiltedMaterial = GetComponentInParent<Plant>().wiltedMaterial;
    }

    public void ResetGrowStep()
    {
        GetComponent<Renderer>().material = material;
    }

    public void Wilt()
    {
        renderer.material = wiltedMaterial;
    }
}
