using UnityEngine;

public class FlashLightScript : MonoBehaviour
{
    private Transform parentTransform;
    private Light lightSource;
    private float charge;
    private float worktime = 60.0f;
    private AudioSource midSound;
    private AudioSource lastSound;

    public float chargeLevel => charge;

    void Start()
    {
        parentTransform = transform.parent;
        if(parentTransform == null )
        {
            Debug.LogError("FlashLightScript: parentTransform not found");
        }
        lightSource = GetComponent<Light>();
        charge = 1.0f;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        midSound = audioSources[0];
        lastSound = audioSources[1];
        //GameState.Subscribe(OnBatteryEvent, "Battery");
    }

    void Update()
    {
        if (parentTransform == null) return;

        if (charge > 0 && !GameState.isDay)
        {
            lightSource.intensity = chargeLevel;
            charge -= Time.deltaTime / worktime;
            if(charge <= 0.5f && charge >= 0.49f)
            {
                midSound.Play();
                lightSource.intensity = 0;
                
            }
            if (charge <= 0.10f && charge >= 0.09f)
            {
                lastSound.Play();
            }
        }

        if (GameState.isFpv)
        {
            transform.forward = Camera.main.transform.forward;
        }
        else
        {
            Vector3 f = Camera.main.transform.forward;
            f.y = 0.0f;
            if(f == Vector3.zero) f = Camera.main.transform.up; 
            transform.forward = f.normalized;
        }
    }

    //private void OnBatteryEvent(string eventName, object data)
    //{
    //    if(data is GameEvents.MessageEvent e)
    //    {
    //        charge += (float)e.data;
    //    }
    //}
    
    //private void OnDestroy(string eventName, object data)
    //{
    //    GameState.UnSubscribe(OnBatteryEvent, "Battery");
    //}

    public void RefillCharge(float chargeValue)
    {
        charge = chargeValue;
    }
}
