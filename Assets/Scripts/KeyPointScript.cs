using UnityEngine;

public class KeyPointScript : MonoBehaviour
{
    [SerializeField]
    private string keyPointName = "1";
    [SerializeField]
    private float timeout = 2.0f;
    [SerializeField]
    private int room = -1;

    private float leftTime;
    public float part { get; private set; }

    private AudioSource collectSound;
    private float destroyTimeout;

    void Start()
    {
        leftTime = timeout;
        part = 1.0f;
        collectSound = GetComponent<AudioSource>();

        GameState.Subscribe(OnSoundsVolumeTrigger, "EffectsVolume");
    }
    
    void Update()
    {
        if (GameState.room == room && leftTime > 0) { 
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
            GameState.collectedItems.Add("Key" + keyPointName, part);
            GameState.TriggerGameEvent("KeyPoint", new GameEvents.MessageEvent
            {
                message = "Знайдено ключ " + keyPointName,
                data = part
            });

            destroyTimeout = .5f;
        }
    }

    private void OnSoundsVolumeTrigger(string eventName, object data)
    {
        if (eventName == "EffectsVolume")
        {
            collectSound.volume = (float)data;
        }
    }

    private void OnDestroy()
    {
        GameState.UnSubscribe(OnSoundsVolumeTrigger, "EffectsVolume");
    }
}
