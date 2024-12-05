using UnityEngine;
using UnityEngine.UI;

public class BatteryIndicatorScript : MonoBehaviour
{
    private Image image;
    private FlashLightScript flashlightScript;
    void Start()
    {
        image = GetComponent<Image>();
        flashlightScript = GameObject
            .Find("FlashLight")
            .GetComponent<FlashLightScript>();
    }


    void Update()
    {
        image.fillAmount = flashlightScript.chargeLevel;
        image.color = new Color(
            1f - image.fillAmount,
            image.fillAmount,
            0.5f
        );
    }
}
