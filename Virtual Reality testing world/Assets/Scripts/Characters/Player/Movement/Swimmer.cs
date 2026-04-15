using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Swimmer : MonoBehaviour
{
    //the code in this file was heavily infulenced by this tutorial by Justin P Barnett: https://www.youtube.com/watch?v=ViQzKZvYdgE 

    [Header("Values")]
    [SerializeField] float swimForce = 2f;
    [SerializeField] float dragForce = 1;
    [SerializeField] float minForce;
    [SerializeField] float minTimeBetweenStrokes;


    [Header("References")]
    [SerializeField] InputActionReference leftControllerSwimReference;
    [SerializeField] InputActionReference leftControllerVelocity;
    [SerializeField] InputActionReference rightControllerSwimReference;
    [SerializeField] InputActionReference rightControllerVelocity;

    [SerializeField] Transform trackingReference;

    Rigidbody rb;
    float cooldownTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer > minTimeBetweenStrokes && leftControllerSwimReference.action.IsPressed() && rightControllerSwimReference.action.IsPressed())
        {
            var leftHandVelocity = leftControllerVelocity.action.ReadValue<Vector3>();
            var rightHandVelocity = rightControllerVelocity.action.ReadValue<Vector3>();
            Vector3 localVelocity = leftHandVelocity + rightHandVelocity;
            localVelocity *= -1;

            if (localVelocity.sqrMagnitude > minForce * minForce)
            {
                Vector3 worldVelocity = trackingReference.TransformDirection(localVelocity);
                rb.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
            }
        }

        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            rb.AddForce(-rb.linearVelocity * dragForce, ForceMode.Acceleration);
        }
    }

}
