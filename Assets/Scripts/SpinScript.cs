using UnityEngine;

public class SpinScript : MonoBehaviour
{
    [SerializeField]
    private float period = 5.0f;

    [SerializeField]
    private bool x = false;
    [SerializeField]
    private bool y = true;
    [SerializeField]
    private bool z = false;

    void Start()
    {
        
    }


    void Update()
    {
        this.transform.Rotate(
            x ? Time.deltaTime / period * 360 : 0,
            y ? Time.deltaTime / period * 360 : 0,
            z ? Time.deltaTime / period * 360 : 0, 
            Space.World);
    }
}
