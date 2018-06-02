using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManager : ISingleton<TimeManager>
{

    protected TimeManager()
    {
    }

    public bool gameIsPaused;

    public delegate void voidNoParam();
    public voidNoParam OnNewDayEvent;
    public voidNoParam OnNewDayMidEvent;
    public voidNoParam OnNewDayLateEvent;

    public float secondPerDay;
    public int dayPerSeason;

    private float secondPerHour, secondPerMinute, secondPerSecond;

    private double realTime;

    public Clock clock;
    public Calendar calendar;
    public int dayPassed;

    public Clock wakeUp;

    private bool isDay = false;



	private void Start()
	{
        secondPerHour = secondPerDay / 24;
        secondPerMinute = secondPerHour / 60;
        secondPerSecond = secondPerMinute / 60;
	}

	private void Update()
    {
        CalculateTime();
    }

    public void NextDay()
    {
        Debug.Log("Next Day");

        if (OnNewDayEvent != null)         //updateUI
        {
            OnNewDayEvent.Invoke();
        }

        if (OnNewDayMidEvent != null)         //updateUI
        {
            OnNewDayMidEvent.Invoke();
        }

        if (OnNewDayLateEvent != null)         //updateUI
        {
            OnNewDayLateEvent.Invoke();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Play()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void CalculateTime()
    {
        realTime += Time.deltaTime;
        if(realTime >= secondPerSecond)
        {
            int s = Mathf.FloorToInt((float)(realTime / secondPerSecond));
            realTime -= s * secondPerSecond;
            if(clock.AddSecond(s))
            {
                calendar.AddDay();
                NextDay();

                if(dayPassed >= dayPerSeason)
                {
                    calendar.AddSeason();
                }
            }
        }
    }

    public float TimeNormalised()
    {
        float s = clock.second / 60;
        float m = (clock.minute + s) / 60;
        float h = (clock.hour + m) / 24;
        return h;
    }
    
}
