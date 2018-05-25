using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWithClock : MonoBehaviour 
{
    public Clock start, end;
    public bool disable;
    private bool isActive = true;


	private void Start()
	{
        if (TimeManager.Instance.clock > start && TimeManager.Instance.clock < end) Toggle(!disable);
        else Toggle(disable);
	}

	// Update is called once per frame
	void Update () 
    {
        if(disable == isActive && TimeManager.Instance.clock > start && TimeManager.Instance.clock < end)
        {
            Toggle(!disable);
        }
        if(disable != isActive)
        {
            if (TimeManager.Instance.clock < start || TimeManager.Instance.clock > end)
            {
                Toggle(disable);
            }
        }
	}

    public void Toggle(bool active)
    {
        isActive = active;
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
