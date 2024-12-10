using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    private GameObject content;

    private Slider effectsSlider;
    private Slider ambientSlider;
    private Slider musicSlider;
    private Toggle soundsToggle;

    private Slider sensitivityXSlider;
    private Slider sensitivityYSlider;
    private Slider fpvSlider;
    private Toggle linkToggle;


    void Start()
    {
        Transform contentTransform = transform.Find("Content");
        content = contentTransform.gameObject;
        if(content.activeInHierarchy)
        {
            Time.timeScale = 0.0f;
        }

        effectsSlider = contentTransform.Find("Sounds/EffectsSlider").GetComponent<Slider>();
        ambientSlider = contentTransform.Find("Sounds/AmbientSlider").GetComponent<Slider>();
        musicSlider   = contentTransform.Find("Sounds/MusicSlider").GetComponent<Slider>();
        soundsToggle = contentTransform.Find("Sounds/SoundsToggle").GetComponent<Toggle>();
        OnEffectsSliderChanged(effectsSlider.value);
        OnAmbientSliderChanged(ambientSlider.value);
        OnMusicSliderChanged(musicSlider.value);
        OnSoundsToggleChanged();

        sensitivityXSlider = contentTransform.Find("Controls/SensXSlider").GetComponent<Slider>();
        sensitivityYSlider = contentTransform.Find("Controls/SensYSlider").GetComponent<Slider>();
        linkToggle = contentTransform.Find("Controls/LinkToggle").GetComponent<Toggle>();
        OnSensitivityXSliderChanged(sensitivityXSlider.value);
        if(!linkToggle.isOn) OnSensitivityYSliderChanged(sensitivityYSlider.value);

        fpvSlider = contentTransform.Find("Controls/FpvSlider").GetComponent<Slider>();
        OnFpvSliderChanged(fpvSlider.value);

        LoadSettings();
    }
    
    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(nameof(effectsSlider)))
        {
            OnEffectsSliderChanged(
                PlayerPrefs.GetFloat(nameof(effectsSlider))
            );
        }
        if (PlayerPrefs.HasKey(nameof(ambientSlider)))
        {
            OnAmbientSliderChanged(
                PlayerPrefs.GetFloat(nameof(ambientSlider))
            );
        }
        if (PlayerPrefs.HasKey(nameof(musicSlider)))
        {
            OnMusicSliderChanged(
                PlayerPrefs.GetFloat(nameof(musicSlider))
            );
        }
        if (PlayerPrefs.HasKey(nameof(soundsToggle)))
        {
            soundsToggle.isOn =
                PlayerPrefs.GetInt(nameof(soundsToggle)) == 1;
        }

        if (PlayerPrefs.HasKey(nameof(linkToggle)))
        {
            linkToggle.isOn =
                PlayerPrefs.GetInt(nameof(linkToggle)) == 1;
        }
        if (PlayerPrefs.HasKey(nameof(sensitivityXSlider)))
        {
            OnSensitivityXSliderChanged(
                PlayerPrefs.GetFloat(nameof(sensitivityXSlider))
            );
        }
        if (PlayerPrefs.HasKey(nameof(sensitivityYSlider)))
        {
            OnSensitivityYSliderChanged(
                PlayerPrefs.GetFloat(nameof(sensitivityYSlider))
            );
        }
        if (PlayerPrefs.HasKey(nameof(fpvSlider)))
        {
            OnFpvSliderChanged(
                PlayerPrefs.GetFloat(nameof(fpvSlider))
            );
        }

    }

    void Update()
    {
        Time.timeScale = content.activeInHierarchy ?  0.0f : 1.0f ;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //Time.timeScale = content.activeInHierarchy ? 1.0f  : 0.0f;
            content.SetActive( ! content.activeInHierarchy );
        }
    }

    public void OnEffectsSliderChanged(float value)
    {
        soundsToggle.isOn = false;
        effectsSlider.value = value;
        GameState.TriggerGameEvent("EffectsVolume", GameState.effectsVolume = value);
    }
    public void OnAmbientSliderChanged(float value)
    {
        ambientSlider.value = value;
        GameState.TriggerGameEvent("AmbientVolume", GameState.ambientVolume = value);
    }
    public void OnMusicSliderChanged(float value)
    {
        musicSlider.value = value;
        GameState.TriggerGameEvent("MusicVolume", GameState.musicVolume = value);
    }
    public void OnSoundsToggleChanged()
    {
        if(soundsToggle.isOn)
        {
            GameState.TriggerGameEvent("EffectsVolume", GameState.effectsVolume = 0);
            GameState.TriggerGameEvent("AmbientVolume", GameState.ambientVolume = 0);
            GameState.TriggerGameEvent("MusicVolume", GameState.effectsVolume = 0);
        }
        else
        {
            GameState.TriggerGameEvent("EffectsVolume", GameState.effectsVolume = effectsSlider.value);
            GameState.TriggerGameEvent("AmbientVolume", GameState.ambientVolume = ambientSlider.value);
            GameState.TriggerGameEvent("MusicVolume", GameState.musicVolume = musicSlider.value);

        }
    }

    public void OnSensitivityXSliderChanged(float value)
    {
        float sens = Mathf.Lerp(1, 10, value);
        GameState.lookSensitivityX = sens;
        sensitivityXSlider.value = value;
        if (linkToggle.isOn)
        {
            sensitivityYSlider.value = value;
            GameState.lookSensitivityY = -sens;
        }
    }
    public void OnSensitivityYSliderChanged(float value)
    {
        float sens = Mathf.Lerp(1, 10, value);
        GameState.lookSensitivityY = -sens;
        sensitivityYSlider.value = value;
        if (linkToggle.isOn)
        {
            sensitivityXSlider.value = value;
            GameState.lookSensitivityX = sens;
        }
    }
    public void OnFpvSliderChanged(float value)
    {
        fpvSlider.value = value;
        GameState.minFpvDistance = Mathf.Lerp(0.5f, 1.5f, value);
    }

    public void OnSaveButtonClick()
    {
        PlayerPrefs.SetFloat(nameof(effectsSlider), effectsSlider.value);
        PlayerPrefs.SetFloat(nameof(ambientSlider), ambientSlider.value);
        PlayerPrefs.SetFloat(nameof(musicSlider), musicSlider.value);
        PlayerPrefs.SetInt(nameof(soundsToggle), soundsToggle.isOn ? 1 : 0);

        PlayerPrefs.SetFloat(nameof(sensitivityXSlider), sensitivityXSlider.value);
        PlayerPrefs.SetFloat(nameof(sensitivityYSlider), sensitivityYSlider.value);
        PlayerPrefs.SetInt(nameof(linkToggle), linkToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat(nameof(fpvSlider), fpvSlider.value);

        PlayerPrefs.Save();
    }

    public void OnDefaultButtonClick()
    {
        effectsSlider.value = 1;
        ambientSlider.value = 0.5f;
        musicSlider.value = 0.05f;
        soundsToggle.isOn = false;
        linkToggle.isOn = true;
        sensitivityXSlider.value = 0.8f;
        fpvSlider.value = 0.5f;
    }
}
