using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWithClock : MonoBehaviour 
{
    public Clock start, end;
    public bool disable;
    private bool isActive = true;

    public bool isDisabled;


	private void Start()
	{
        if (TimeManager.Instance.clock > start && TimeManager.Instance.clock < end) Disable(disable);
        else Disable(!disable);
	}

	// Update is called once per frame
	void Update () 
    {
        if(isDisabled == disable)
        {
            if (TimeManager.Instance.clock < start || TimeManager.Instance.clock > end)
            {
                Disable(!disable);
            }
        }
        else
        {
            if(TimeManager.Instance.clock > start)
            {
                Disable(disable);
            }
        }
	}

    public void Disable(bool state)
    {
        isDisabled = state;
        state = !state;
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(state);
        }
    }
}
