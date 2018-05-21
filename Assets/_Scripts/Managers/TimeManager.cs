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
    public voidNoParam OnNewDayLateEvent;

    public float secondPerDay;
    public int dayPerSeason;

    private float secondPerHour, secondPerMinute, secondPerSecond;

    private double realTime;

    public Clock clock;
    public Calendar calendar;
    public int dayPassed;

    [Header("Song timers")]
    public Clock dayBeginning;
    public Clock nightBeginning;
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

                if(dayPassed >= dayPerSeason)
                {
                    calendar.AddSeason();
                }
            }
        }
    }
    
}
