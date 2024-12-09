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
        musicSlider = contentTransform.Find("Sounds/MusicSlider").GetComponent<Slider>();
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

        LoadSettings();
    }
    
    private void LoadSettings()
    {
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
        if (PlayerPrefs.HasKey(nameof(linkToggle)))
        {
            linkToggle.isOn =
                PlayerPrefs.GetInt(nameof(linkToggle)) == 1;
        }

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
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = content.activeInHierarchy ? 1.0f  : 0.0f;
            content.SetActive( ! content.activeInHierarchy );
        }
    }

    public void OnEffectsSliderChanged(float value)
    {
        GameState.TriggerGameEvent("EffectsVolume", GameState.effectsVolume = value);
    }
    public void OnAmbientSliderChanged(float value)
    {
        GameState.TriggerGameEvent("AmbientVolume", GameState.ambientVolume = value);
    }
    public void OnMusicSliderChanged(float value)
    {
        GameState.TriggerGameEvent("MusicVolume", GameState.musicVolume = value);
    }
    public void OnSoundsToggleChanged()
    {
        if(soundsToggle.isOn)
        {
            effectsSlider.value = 0;
            ambientSlider.value = 0;
            musicSlider.value = 0;
        }
        //else
        //{
        //    effectsSlider.value = GameState.effectsVolume;
        //    ambientSlider.value = GameState.ambientVolume;
        //    musicSlider.value   = GameState.musicVolume;
        //}
    }

    public void OnSensitivityXSliderChanged(float value)
    {
        float sens = Mathf.Lerp(1, 10, value);
        GameState.lookSensitivityX = sens;
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
        if (linkToggle.isOn)
        {
            sensitivityXSlider.value = value;
            GameState.lookSensitivityX = sens;
        }
    }

    public void OnSaveButtonClick()
    {
        PlayerPrefs.SetFloat(nameof(sensitivityXSlider), sensitivityXSlider.value);
        PlayerPrefs.SetFloat(nameof(sensitivityYSlider), sensitivityYSlider.value);
        PlayerPrefs.SetInt(nameof(linkToggle), linkToggle.isOn ? 1 : 0);

        PlayerPrefs.SetFloat(nameof(effectsSlider), effectsSlider.value);
        PlayerPrefs.SetFloat(nameof(ambientSlider), ambientSlider.value);
        PlayerPrefs.SetFloat(nameof(musicSlider), musicSlider.value);
        PlayerPrefs.SetInt(nameof(soundsToggle), soundsToggle.isOn ? 1 : 0);

        PlayerPrefs.Save();
    }
}
