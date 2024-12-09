using System.Linq;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    private Light[] daylights;
    private Light[] nightlights;

    private AudioSource daySound;
    private AudioSource nightSound;
    private AudioSource musicBack;

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
        musicBack = audioSources[2];
        
        GameState.Subscribe(OnAmbientVolumeTrigger, "AmbientVolume");
        GameState.Subscribe(OnMusicVolumeTrigger, "MusicVolume");
        SwitchLight();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N)) 
        {
            SwitchLight();
        }
    }

    private void OnAmbientVolumeTrigger(string eventName, object data)
    {
        if(eventName == "AmbientVolume")
        {
            daySound.volume = (float)data;
            nightSound.volume = (float)data;
        }
    }

    private void OnMusicVolumeTrigger(string eventName, object data)
    {
        if (eventName == "MusicVolume")
        {
            musicBack.volume = (float)data;
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

    private void OnDestroy()
    {
        GameState.UnSubscribe(OnAmbientVolumeTrigger, "AmbientVolume");
        GameState.UnSubscribe(OnMusicVolumeTrigger, "MusicVolume");
    }
}
