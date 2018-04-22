using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Clock : IEquatable<Clock>
{
    public int second;
    public int minute;
    public int hour;
    public int day;

    public Clock (int _day, int _hour, int _minute, int _second)
    {
        second = _second;
        minute = _minute;
        hour = _hour;
        day = _day;
    }

	public bool Equals(Clock other)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
	{
        return "Day " + day + ", Hour " + hour + ", Minute " + minute + ", Second " + second;
	}

    public static bool operator == (Clock a, Clock b)
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
        return (int)a.second == (int)b.second && (int)a.minute == (int)b.minute && (int)a.hour == (int)b.hour;
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
                         hour.GetHashCode() +
                         day.GetHashCode();

        }
    }

	public override bool Equals(object obj)
	{
        return Equal((Clock)obj);
	}
    public bool Equal(Clock other)
    {
        return other == this;
    }
}
