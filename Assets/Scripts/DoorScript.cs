using System.Linq;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private string requiredKey;
    private float openingTime = 3.0f;
    private float timeout = 0f;
    private float openedPart = 0.5f;
    private bool isClosed = true;
    private AudioSource closedSound;
    private AudioSource openingSound;

    
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>(); 
        closedSound = audioSources[0];
        openingSound = audioSources[1];

        GameState.Subscribe(OnSoundsVolumeTrigger, "EffectsVolume");
    }

    void Update()
    {
        if (timeout > 0f)
        {
            float t = Time.deltaTime / openingTime;
            transform.Translate(0, 0, -t);
            timeout -= t;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isClosed)
        {
            if (GameState.collectedItems.ContainsKey("Key" + requiredKey))
            {
                bool isInTime = (float)GameState.collectedItems["Key" + requiredKey] > 0;
                GameState.TriggerGameEvent("Door1", new GameEvents.MessageEvent
                {
                    message = "Двері відчиняються " + (isInTime ? "швидко" : "повільно"),
                    data = requiredKey
                });
                if (!isInTime) openingTime *= 3;
                timeout = 1.0f;
                isClosed = false;
                GameState.room += 1; 
                openingSound.Play();
            }
            else
            {
                GameState.TriggerGameEvent("Door1", new GameEvents.MessageEvent
                {
                    message = "Необхідно знайти ключ " + requiredKey,
                    data = requiredKey
                });
                closedSound.Play();
            }
        }
    }

    private void OnSoundsVolumeTrigger(string eventName, object data)
    {
        if (eventName == "EffectsVolume")
        {
            openingSound.volume = (float)data;
            closedSound.volume = (float)data;
        }
    }
    private void OnDestroy()
    {
        GameState.UnSubscribe(OnSoundsVolumeTrigger, "EffectsVolume");
    }
}
