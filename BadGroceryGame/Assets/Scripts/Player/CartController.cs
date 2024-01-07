using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{
    Rigidbody rb;
    public float ForwardVelocity;
    public float ForwardPFactor;
    public float TurnVelocity;
    public float TurnPFactor;
    GameControls controls;

    Vector2 moveAxis;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controls = new GameControls();
        controls.Enable();
        controls.Cart.Enable();
        controls.Cart.Move.performed += OnMoveAxisChanged;
        controls.Cart.Move.canceled += OnMoveAxisChanged;
    }

    private void OnMoveAxisChanged(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        moveAxis = obj.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(moveAxis);

        float currentFSpeed = Vector3.Dot(transform.forward, rb.velocity);
        float goalSpeed = moveAxis.y * ForwardVelocity;
        if((goalSpeed > Mathf.Epsilon && currentFSpeed < goalSpeed) || goalSpeed < -Mathf.Epsilon && currentFSpeed > goalSpeed)
        {
            rb.AddForce(transform.forward * (goalSpeed - currentFSpeed) * ForwardPFactor);
        }

        float currentTurn = Vector3.Dot(transform.up, rb.angularVelocity);
        float goalTurn = moveAxis.x * TurnVelocity;
        if ((goalTurn > Mathf.Epsilon && currentTurn < goalTurn) || goalTurn < -Mathf.Epsilon && currentTurn > goalTurn)
        {
            rb.AddTorque(transform.up * (goalTurn - currentTurn) * TurnPFactor);
        }
    }
}
