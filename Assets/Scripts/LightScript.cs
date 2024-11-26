using System.Linq;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private Light[] daylights;
    private Light[] nightlights;

    void Start()
    {

        daylights = GameObject
            .FindGameObjectsWithTag("DayLight")
            .Select(g=> g.GetComponent<Light>())
            .ToArray();
        nightlights = GameObject
            .FindGameObjectsWithTag("NightLight")
            .Select(g => g.GetComponent<Light>())
            .ToArray();

        SwitchLight();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N)) 
        {
            SwitchLight();
        }
    }

    private void SwitchLight()
    {
        GameState.isDay = !GameState.isDay;
        foreach (Light light in daylights)
        {
            light.enabled = GameState.isDay;
        }
        foreach (Light light in nightlights)
        {
            light.enabled = !GameState.isDay;
        }
    }
}
