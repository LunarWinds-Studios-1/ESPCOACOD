using UnityEngine;



public class DistanceJoint3D : MonoBehaviour
{
    public Rigidbody ConnectedRigidbody;
    [SerializeField] bool DetermineDistanceOnStart = true;
    public float distance;

    protected Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (DetermineDistanceOnStart)
        {
            distance = Vector3.Distance(rb .position, ConnectedRigidbody.position);
        }
    }

    private void FixedUpdate()
    {
        var connection = ConnectedRigidbody.position - rb.position;
        var distanceDiscrepancy = distance - connection.magnitude;

        ConnectedRigidbody.position += distanceDiscrepancy * connection.normalized;

        var velocityTarget = connection + ConnectedRigidbody.linearVelocity;
        var projectOnConnection = Vector3.Project(velocityTarget, connection);
        ConnectedRigidbody.linearVelocity = (velocityTarget - projectOnConnection);
    }
}