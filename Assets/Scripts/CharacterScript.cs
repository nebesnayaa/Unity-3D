using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    private Rigidbody rb;
    private InputAction moveAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        rb.AddForce(150 * Time.deltaTime * new Vector3(moveValue.x, 0, moveValue.y));

        //Vector2 axisValue = new Vector2(
        //    Input.GetAxis("Horizontal"),
        //    Input.GetAxis("Vertical"));
        //if (moveValue != Vector2.zero)
        //{
        //    Debug.Log(moveValue);
        //    Debug.Log(axisValue);
        //    Debug.Log("---");
        //}
    }
}
