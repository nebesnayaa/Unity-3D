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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameState.collectedItems.ContainsKey("Key1" + requiredKey))
            {
                GameState.TriggerGameEvent("Door1", 
                    new GameEvents.MessageEvent { 
                        message = "Двері відчиняються",
                        data = requiredKey
                    });
                timeout = openingTime;
                openingSound.Play();
            }
            else {
                GameState.TriggerGameEvent("Door1", new GameEvents.MessageEvent
                {
                    message = "Необхідно знайти ключ " + requiredKey,
                    data = requiredKey
                });
                closedSound.Play();
            }
        }
    }

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>(); 
        closedSound = audioSources[0];
        openingSound = audioSources[1];
    }

    void Update()
    {
        if (timeout > 0f)
        {
            transform.Translate(0, 0, -Time.deltaTime / openingTime);
            timeout -= Time.deltaTime;
        }
    }
}
