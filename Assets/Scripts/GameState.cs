using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static bool isDay { get; set; }
    public static bool isFpv { get; set; }

    #region Game event
    // Emit Event
    public static void TriggerGameEvent(String eventName )
    {
        foreach (var action in subscribers[eventName])
        {
            action(eventName);
        }
    }

    private static Dictionary<string, List<Action<String>>> subscribers = new();
    public static void Subscribe(Action<String> action, String eventName)
    {

    }
    public static void UnSubscribe(Action<String> action, String eventName)
    {

    }
    #endregion
}
