using System;
using System.Text.RegularExpressions;
using UnityEngine;

class Format
{
    public static string Time(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        if(time.Hours > 0)
        {
            return string.Format("{0:D2}h {1:D2}m {2:D2}s {3:D3}ms", 
                    time.Hours, 
                    time.Minutes, 
                    time.Seconds, 
                    time.Milliseconds);
        }
        else if(time.Minutes > 0)
        {
            return string.Format("{0:D2}m {1:D2}s {2:D3}ms", 
                    time.Minutes, 
                    time.Seconds, 
                    time.Milliseconds);
        }
        else
        {
            return string.Format("{0:D2}s {1:D3}ms", 
                    time.Seconds, 
                    time.Milliseconds);
        }
    }

}