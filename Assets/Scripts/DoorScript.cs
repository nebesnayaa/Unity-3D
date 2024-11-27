using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            ToastScript.ShowToast("Необхідно знайти ключ №1", author: "Двері 1");
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }

}
