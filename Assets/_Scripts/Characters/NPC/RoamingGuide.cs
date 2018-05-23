using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingGuide : Interactable 
{
    private float timer;
    public float timerMin, timerMax;
    private Vector3[] childs;

	private void Start()
	{
        childs = new Vector3[transform.childCount];
        int i = 0;
        foreach(Transform child in transform)
        {
            childs[i] = child.position;
            i++;
        }
	}

	protected override void Update()
	{
        if (Time.time > timer)
        {
            int r = (int)Random.Range(0, childs.Length);
            transform.position = childs[r];
            timer = Time.time + Random.Range(timerMin, timerMax);
        }
	}
	
}
