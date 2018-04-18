using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : ISingleton<TimeManager> {

    protected TimeManager() {}

    public delegate void voidNoParam();
    public voidNoParam OnNewDayEvent;
    public voidNoParam OnNewDayLateEvent;



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
}
