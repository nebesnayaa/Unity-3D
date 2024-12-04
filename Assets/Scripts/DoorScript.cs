using System.Linq;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private string requiredKey = "1";
    private float openingTime = 3.0f;
    private float timeout = 0f;

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
            }
            else {
                GameState.TriggerGameEvent("Door1", new GameEvents.MessageEvent
                {
                    message = "Необхідно знайти ключ " + requiredKey,
                    data = requiredKey
                });
            }
        }
    }

    void Start()
    {

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
