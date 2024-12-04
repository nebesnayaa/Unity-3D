using UnityEngine;
using UnityEngine.UI;

public class BatteryIndicatorScript : MonoBehaviour
{
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {
        image.fillAmount = 1f;
        image.color = new Color(
            1f - image.fillAmount,
            image.fillAmount - 1f,
            0.5f
        );
    }
}
