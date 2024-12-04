using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    [SerializeField]
    private string batteryName = "1";
    [SerializeField]
    private float timeout = 2.0f;
    private float leftTime;
    public float part;

    FlashLightScript flashLightScript;
    

    void Start()
    {
        leftTime = timeout;
        part = 1.0f;

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(GameObject.Find("Character")))
        {
            GameState.collectedItems.Add("Battery" + batteryName, part);
            GameState.TriggerGameEvent("Батарейку " + batteryName + " знайдено", new GameEvents.MessageEvent
            {
                message = "Батарейку " + batteryName + " знайдено",
                data = part
            });
            flashLightScript.RefillCharge();

            GameObject.Destroy(this.gameObject);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        GameState.collectedItems.Add("Battery" + batteryName, part);
    //        GameState.TriggerGameEvent("Батарейку " + batteryName + " знайдено", new GameEvents.MessageEvent
    //        {
    //            message = "Батарейку " + batteryName + " знайдено",
    //            data = part
    //        });

    //        flashLightScript.RefillCharge();

    //        Destroy(gameObject);
    //    }
    //}
}
