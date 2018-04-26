using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
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

    public override string ToString()
    {
        return "Hour " + hour + ", Minute " + minute + ", Second " + second;
    }

    public static bool operator ==(Clock a, Clock b)
    {
        // an item is always equal to itself
        if (object.ReferenceEquals(a, b))
            return true;

        // if both a and b were null, we would have already escaped so check if either is null
        if (object.ReferenceEquals(a, null))
            return false;
        if (object.ReferenceEquals(b, null))
            return false;
        // Now that we've made sure we are working with real objects:
        return a.second == b.second && a.minute == b.minute && a.hour == b.hour;
    }

    public static bool operator !=(Clock a, Clock b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return second.GetHashCode() +
                         minute.GetHashCode() +
                         hour.GetHashCode();
        }
    }

    public override bool Equals(object obj)
    {
        return Equals((Clock)obj);
    }
    public bool Equals(Clock other)
    {
        return other == this;
    }

    public static bool operator >=(Clock a, Clock b)
    {
        // an item is always equal to itself
        if (object.ReferenceEquals(a, b))
            return true;

        // if both a and b were null, we would have already escaped so check if either is null
        if (object.ReferenceEquals(a, null))
            return false;
        if (object.ReferenceEquals(b, null))
            return false;
        // Now that we've made sure we are working with real objects:

        if (a.hour > b.hour) return true;
        else if (a.hour == b.hour)
        {
            if (a.minute > b.hour) return true;
            else if (a.minute == b.minute)
            {
                if (a.second >= b.second) return true;
            }
        }

        return false;
    }

    public static bool operator <=(Clock a, Clock b)
    {
        // an item is always equal to itself
        if (object.ReferenceEquals(a, b))
            return true;

        // if both a and b were null, we would have already escaped so check if either is null
        if (object.ReferenceEquals(a, null))
            return false;
        if (object.ReferenceEquals(b, null))
            return false;
        // Now that we've made sure we are working with real objects:

        if (a.hour < b.hour) return true;
        else if (a.hour == b.hour)
        {
            if (a.minute < b.hour) return true;
            else if (a.minute == b.minute)
            {
                if (a.second <= b.second) return true;
            }
        }

        return false;
    }

    public static bool operator >(Clock a, Clock b)
    {
        // an item is always equal to itself
        if (object.ReferenceEquals(a, b))
            return true;

        // if both a and b were null, we would have already escaped so check if either is null
        if (object.ReferenceEquals(a, null))
            return false;
        if (object.ReferenceEquals(b, null))
            return false;
        // Now that we've made sure we are working with real objects:

        if (a.hour > b.hour) return true;
        else if (a.hour == b.hour)
        {
            if (a.minute > b.hour) return true;
            else if (a.minute == b.minute)
            {
                if (a.second > b.second) return true;
            }
        }

        return false;
    }

    public static bool operator <(Clock a, Clock b)
    {
        // an item is always equal to itself
        if (object.ReferenceEquals(a, b))
            return true;

        // if both a and b were null, we would have already escaped so check if either is null
        if (object.ReferenceEquals(a, null))
            return false;
        if (object.ReferenceEquals(b, null))
            return false;
        // Now that we've made sure we are working with real objects:

        if (a.hour < b.hour) return true;
        else if (a.hour == b.hour)
        {
            if (a.minute < b.hour) return true;
            else if (a.minute == b.minute)
            {
                if (a.second < b.second) return true;
            }
        }

        return false;
    }
}
