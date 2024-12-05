using System.Linq;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private Light[] daylights;
    private Light[] nightlights;

    private AudioSource daySound;
    private AudioSource nightSound;

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

        AudioSource[] audioSources = GetComponents<AudioSource>();
        daySound = audioSources[0];
        nightSound = audioSources[1];

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
        if (GameState.isDay)
        {
            nightSound.Stop();
            daySound.Play();
        }
        else
        {
            daySound.Stop();
            nightSound.Play();
        }
    }
}
