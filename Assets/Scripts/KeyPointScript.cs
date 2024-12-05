using UnityEngine;

public class KeyPointScript : MonoBehaviour
{
    [SerializeField]
    private string keyPointName = "1";
    [SerializeField]
    private float timeout = 2.0f;
    private float leftTime;

    public float part;

    private AudioSource collectSound;
    private float destroyTimeout;


    void Start()
    {
        leftTime = timeout;
        part = 1.0f;
        collectSound = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (leftTime > 0) { 
            leftTime -= Time.deltaTime / timeout;
            if( leftTime < 0) leftTime = 0;
            part = leftTime / timeout;
        }
        if (destroyTimeout > 0)
        {
            destroyTimeout -= Time.deltaTime;
            if (destroyTimeout <= 0)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collectSound.Play();
            GameState.collectedItems.Add("Key1" + keyPointName, part);
            GameState.TriggerGameEvent("KeyPoint", new GameEvents.MessageEvent
            {
                message = "Знайдено ключ " + keyPointName,
                data = part
            });
            //Destroy(gameObject);
            destroyTimeout = .8f;
        }
    }
}
