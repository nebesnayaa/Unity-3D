using UnityEngine;
using UnityEngine.UI;

public class BatteryTimerImageScript : MonoBehaviour
{
    private Image image;
    private BatteryScript batteryScript;

    void Start()
    {
        image = GetComponent<Image>();
        Transform t = this.transform;
        while(t != null && 
            (batteryScript = t.gameObject.GetComponent<BatteryScript>()) == null)
        {
            t = t.parent;
        }
        if(batteryScript == null)
        {
            Debug.LogError("TimerImageScript: BatteryScript not found in parent");
        }
    }


    void Update()
    {
        if (batteryScript != null)
        {
            image.fillAmount = batteryScript.part;
            image.color = new Color(
                1 - image.fillAmount,
                image.fillAmount,
                0.2f
            );
        }
    }
}
