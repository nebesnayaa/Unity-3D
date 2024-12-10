using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public static bool isDay { get; set; }
    public static bool isFpv { get; set; }
    public static int room { get; set; } = 1;

    public static Dictionary<String, object> collectedItems { get; set; } = new();

    public static float effectsVolume { get; set; }
    public static float ambientVolume { get; set; }
    public static float musicVolume { get; set; }

    public static float lookSensitivityX { get; set; } = 10.0f;
    public static float lookSensitivityY { get; set; } = -5.0f;

    public static float minFpvDistance { get; set; } = 0.5f;

    #region Game events
    private const string broadcastKey = "Broadcast";
    public static void TriggerGameEvent(String eventName, object data )
    {
        if (subscribers.ContainsKey(eventName))
        {
            foreach (var action in subscribers[eventName])
            {
                action(eventName, data);
            }
        }
        if (subscribers.ContainsKey(broadcastKey))
        {
            foreach (var action in subscribers[broadcastKey])
            {
                action(eventName, data);
            }
        }
    }

    private static Dictionary<string, List<Action<String, object>>> subscribers = new();

    public static void Subscribe(Action<String, object> action, String eventName = null)
    {
        eventName ??= broadcastKey;
        if (subscribers.ContainsKey(eventName))
        {
            subscribers[eventName].Add(action);
        }
        else
        {
            subscribers[eventName] = new() { action };
        }
    }

    public static void UnSubscribe(Action<String, object> action, String eventName = null)
    {
        eventName ??= broadcastKey;
        if (subscribers.ContainsKey(eventName))
        {
            subscribers[eventName].Remove(action);
        }
        else Debug.LogWarning("Unsubscribe of not subscribed key: " + eventName);
    }
    #endregion
}
