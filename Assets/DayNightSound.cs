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
        float minutes = TimeManager.Instance.clock.minute / 60.0f;
        Debug.Log(minutes);
        float time = TimeManager.Instance.clock.hour + minutes;
        emitter.SetParameterValue("DayHours", time);
	}
}
