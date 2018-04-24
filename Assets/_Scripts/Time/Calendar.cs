using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Day
{
    MONDAY,
    TUESDAY,
    WEDNESDAY,
    THURSDAY,
    FRIDAY,
    SATURDAY,
    SUNDAY
}

public enum Season
{
    SPRING,
    SUMMER,
    AUTUMN,
    WINTER
}

public static class Extensions
{

    public static T Next<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argumnent {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(Arr, src) + 1;
        return (Arr.Length == j) ? Arr[0] : Arr[j];
    }
}

public struct Calendar
{
    public Day day;
    public Season season;
    public int dayPerSeason;

    public void AddDay()
    {
        day = day.Next(); 
    }

    public void AddSeason()
    {
        season = season.Next();
    }
}
