using UnityEngine;

public class BatteryScript : MonoBehaviour
{
    FlashLightScript flashLightScript;

    void Start()
    {
        GameObject flashlight = GameObject.Find("FlashLight");
        if (flashlight != null)
        {
            flashLightScript = flashlight.GetComponent<FlashLightScript>();
        }
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.Equals(GameObject.Find("Character")))
        {
            flashLightScript.RefillCharge();

            GameObject.Destroy(this.gameObject);
        }
    }
}
