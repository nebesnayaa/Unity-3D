using UnityEngine;

public class KeyPointScript : MonoBehaviour
{
    [SerializeField]
    private string keyPointName = "1";
    [SerializeField]
    private float timeout = 2.0f;
    private float leftTime;

    public float part;

    void Start()
    {
        leftTime = timeout;
        part = 1.0f;
    }
    
    void Update()
    {
        if (leftTime > 0) { 
            leftTime -= Time.deltaTime / timeout;
            if( leftTime < 0) leftTime = 0;
            part = leftTime / timeout;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameState.collectedItems.Add("Key1" + keyPointName, part);
            GameState.TriggerGameEvent("KeyPoint", new GameEvents.MessageEvent
            {
                message = "Знайдено ключ " + keyPointName,
                data = part
            });
            Destroy(gameObject);
        }
    }
}
