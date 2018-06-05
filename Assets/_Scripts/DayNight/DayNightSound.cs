using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSound : MonoBehaviour 
{
    FmodEventEmitter emitter;
	private void Start()
	{
        emitter = GetComponent<FmodEventEmitter>();
        SoundManager.Instance.PlaySound("UI/Menu", true);
        SoundManager.Instance.PlaySound("Ambience/AmbienceGame");
	}
	private void Update()
	{
        emitter.SetParameterValue("DayHours", TimeManager.Instance.TimeNormalised() * 24);
	}
}
