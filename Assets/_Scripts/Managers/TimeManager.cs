using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : ISingleton<TimeManager>
{

    protected TimeManager()
    {
    }

    public delegate void voidNoParam();
    public voidNoParam OnNewDayEvent;
    public voidNoParam OnNewDayLateEvent;

    public float secondPerDay;

    private float secondPerHour, secondPerMinute;

    private double realTime;

    public Clock clock;


	private void Start()
	{
        secondPerHour = secondPerDay / 24;
        secondPerMinute = secondPerHour / 60;

        clock = new Clock(0, 0, 0, 0);
	}

	private void Update()
    {
        CalculateTime();
        Debug.Log(clock.ToString());
    }

    public void NextDay()
    {
        Debug.Log("Next Day");

        DialogueManager.Instance.PlayerSay("NewDay");

        if (OnNewDayEvent != null)         //updateUI
        {
            OnNewDayEvent.Invoke();
        }

        if (OnNewDayLateEvent != null)         //updateUI
        {
            OnNewDayLateEvent.Invoke();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Play()
    {
        Time.timeScale = 1f;
    }

    private void CalculateTime()
    {
        realTime += Time.deltaTime;
        clock.minute = Mathf.RoundToInt((float)((realTime / secondPerMinute) % 60));
        clock.hour = Mathf.RoundToInt((float)((realTime / secondPerHour) % 24));
        clock.day = Mathf.RoundToInt((float)(realTime / secondPerDay));
    }
    
}
