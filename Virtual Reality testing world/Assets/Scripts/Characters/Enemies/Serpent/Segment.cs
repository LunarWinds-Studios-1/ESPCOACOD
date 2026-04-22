using UnityEngine;

public class Segment : MonoBehaviour
{
    public Rigidbody parent;
    public Serpent serpent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        serpent = FindFirstObjectByType<Serpent>();
        parent  = GetComponent<DistanceJoint3D>()?.ConnectedRigidbody;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(parent.position);
        GetComponent<Rigidbody>().linearVelocity =- transform.forward * serpent.speed / 2;
    }
}
