using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    [SerializeField]
    private string batteryName = "1";
    [SerializeField]
    public float charge = 0.5f;
    [SerializeField]
    private bool isRandomCharge = false;

    [SerializeField]
    private float timeout = 2.0f;
    private float leftTime;
    public float part;

    FlashLightScript flashLightScript;

    private AudioSource collectSound;
    private float destroyTimeout;

    void Start()
    {
        leftTime = timeout;
        part = 1.0f;
        collectSound = GetComponent<AudioSource>();
             
        GameObject flashlight = GameObject.Find("FlashLight");
        if (flashlight != null)
        {
            flashLightScript = flashlight.GetComponent<FlashLightScript>();
        }
    }

    void Update()
    {
        if (leftTime > 0)
        {
            leftTime -= Time.deltaTime / timeout;
            if (leftTime < 0) leftTime = 0;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (isRandomCharge) charge = Random.Range(0.4f, 1.0f);

        if (collision.gameObject.Equals(GameObject.Find("Character")))
        {
            collectSound.Play();
            GameState.collectedItems.Add("Battery" + batteryName, part);
            GameState.TriggerGameEvent($"Знайдено батарейку із зарядом {charge:F1}", new GameEvents.MessageEvent
            {
                message = $"Знайдено батарейку із зарядом {charge:F1}",
                data = part
            });
            flashLightScript.RefillCharge(charge);

            //GameObject.Destroy(this.gameObject);
            destroyTimeout = .3f;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (isRandomCharge) charge = Random.Range(0.3f, 1.0f);

    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        GameState.TriggerGameEvent("Battery", new GameEvents.MessageEvent
    //        {
    //            message = $"Знайдено батарейку із зарядом {charge:F1}",
    //            data = charge
    //        });

    //        //flashLightScript.RefillCharge();

    //        Destroy(gameObject);
    //    }
    //}
}
