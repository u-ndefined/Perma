using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : ISingleton<TimeManager> {

    protected TimeManager() {}

    public delegate void voidNoParam();
    public voidNoParam OnNewDayEvent;

    public void NextDay()
    {
        Debug.Log("Next Day");
        if (OnNewDayEvent != null)         //updateUI
        {
            OnNewDayEvent.Invoke();
        }
    }
}
