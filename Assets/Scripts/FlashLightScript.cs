using UnityEngine;

public class FlashLightScript : MonoBehaviour
{
    private Transform parentTransform;
    private Light lightSource;
    private float charge;
    private float worktime = 60.0f;

    void Start()
    {
        parentTransform = transform.parent;
        if(parentTransform == null )
        {
            Debug.LogError("FlashLightScript: parentTransform not found");
        }
        lightSource = GetComponent<Light>();
        charge = 1.0f;
    }

    void Update()
    {
        if (parentTransform == null) return;

        if (charge > 0 && !GameState.isDay)
        {
            lightSource.intensity = charge;
            charge -= Time.deltaTime / worktime;
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

    public void RefillCharge()
    {
        charge = 1.0f;
    }
}
