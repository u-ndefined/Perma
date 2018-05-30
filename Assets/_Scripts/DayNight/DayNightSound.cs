using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSound : MonoBehaviour 
{
    FmodEventEmitter emitter;
	private void Start()
	{
        emitter = GetComponent<FmodEventEmitter>();
	}
	private void Update()
	{
        emitter.SetParameterValue("DayHours", TimeManager.Instance.TimeNormalised());
	}
}
