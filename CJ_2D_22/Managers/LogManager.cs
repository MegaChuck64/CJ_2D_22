using System;
using System.Collections.Generic;

namespace CJ_2D_22.Managers;

public static class LogManager
{
    public static event EventHandler<LogAddedEventArgs> OnLogAdded;

    public static List<string> Logs = new List<string>();

    public static void AddLog(string msg, string sender = "")
    {
        Logs.Add(msg);
        var eventArgs = new LogAddedEventArgs
        {
            Message = msg,
            Time = DateTime.Now
        };
        OnLogAdded(sender, eventArgs);
    }
}

public class LogAddedEventArgs : EventArgs
{
    public string Message { get; set; }
    public DateTime Time { get; set; }
}