using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            ToastScript.ShowToast("��������� ������ ���� �1", author: "���� 1");
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }

}
