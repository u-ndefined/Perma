using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Clock 
{
    public int second, minute, hour;

    public bool AddSecond(int s)
    {
        second += s;

        if(second >= 60)
        {
            int m = Mathf.FloorToInt(second / 60);
            second -= 60 * m;
            if (AddMinute(m)) return true;
        }

        if (second >= 60) AddSecond(0);

        return false;
    }

    public bool AddMinute(int m)
    {
        minute += m;
        if(minute >= 60)
        {
            int h = Mathf.FloorToInt(minute / 60);
            minute -= 60 * h;
            if(AddHour(h)) return true;
        }
        return false;
    }

    public bool AddHour(int h)
    {
        hour += h;

        if(hour >= 24)
        {
            hour -= 24;
            return true;
        }

        return false;
    }
}
