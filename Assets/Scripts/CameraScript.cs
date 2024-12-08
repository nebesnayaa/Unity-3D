using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    private Transform character; 
    private InputAction lookAction;
    private Vector3 cameraAngles, cameraAngles0;
    private Vector3 r;
    private float sensitivityH = 10.0f;
    private float sensitivityV = -5.0f;

    [SerializeField]
    private float minVerticalAngle;
    [SerializeField]
    private float maxVerticalAngle;
    [SerializeField]
    private float minVerticalAngleFPV;
    [SerializeField]
    private float maxVerticalAngleFPV;

    private float minFpvDistance = 0.9f;
    private float maxFpvDistance = 9.0f;
    private bool isPos3;

    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        cameraAngles0 = cameraAngles = this.transform.eulerAngles;
        character = GameObject.Find("Character").transform;
        r = this.transform.position - character.position;
    }

    void Update()
    {
        Vector2 wheel = Input.mouseScrollDelta;
        if (wheel.y != 0)
        {
            if (r.magnitude >= maxFpvDistance)
            {
                isPos3 = true;
                if (wheel.y > 0)
                {
                    r *= (1 - wheel.y / 10);
                }
            }
            else
            {
                isPos3 = false;
                if (r.magnitude >= minFpvDistance)
                {
                    float rr = r.magnitude * (1 - wheel.y / 10);
                    if (rr <= minFpvDistance)
                    {
                        r *= 0.01f;
                        GameState.isFpv = true;
                    }
                    else
                    {
                        r *= 1 - wheel.y / 10;
                    }
                }
                else
                {
                    if (wheel.y < 0)
                    {
                        r *= 100f;
                        GameState.isFpv = false;
                    }
                }
            }
        }

        if (!isPos3)
        {
            Vector2 lookValue = lookAction.ReadValue<Vector2>();
            if (lookValue != Vector2.zero)
            {
                cameraAngles.x += lookValue.y * Time.deltaTime * sensitivityV;
                cameraAngles.y += lookValue.x * Time.deltaTime * sensitivityH;
                
                cameraAngles.x = Mathf.Clamp(cameraAngles.x,
                                            GameState.isFpv ? minVerticalAngleFPV : minVerticalAngle,
                                            GameState.isFpv ? maxVerticalAngleFPV : maxVerticalAngle);
                
                this.transform.eulerAngles = cameraAngles;
            }
            this.transform.position = character.position +
                Quaternion.Euler(
                    cameraAngles.x - cameraAngles0.x,
                    cameraAngles.y - cameraAngles0.y,
                    0) * r;
        }
    }
}
