using System.Linq;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private string requiredKey = "1";
    private float openingTime = 3.0f;
    private float timeout = 0f;
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
            transform.Translate(0, 0, -Time.deltaTime / openingTime);
            timeout -= Time.deltaTime;
        }
        else if (timeout < 0f) 
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameState.collectedItems.ContainsKey("Key" + requiredKey))
            {
                GameState.TriggerGameEvent("Door1",
                    new GameEvents.MessageEvent
                    {
                        message = "Двері відчиняються",
                        data = requiredKey
                    });
                timeout = openingTime;
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
