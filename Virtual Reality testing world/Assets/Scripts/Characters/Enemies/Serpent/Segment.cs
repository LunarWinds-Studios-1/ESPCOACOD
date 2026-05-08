using UnityEngine;

public class Segment : MonoBehaviour
{
    public Rigidbody parent;
    public Serpent serpent;

    public GameObject bone;

    public Vector3 offsetRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //serpent = FindFirstObjectByType<Serpent>();
        parent  = GetComponent<DistanceJoint3D>()?.ConnectedRigidbody;
        //SoftJointLimit settings = GetComponent<ConfigurableJoint>().linearLimit;
        //settings.limit = Vector3.Distance(transform.position, parent.position);

        offsetRotation = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(parent.position);
        //GetComponent<DistanceJoint3D>().anchor = parent.position;
        //transform.eulerAngles -= offsetRotation;
        if (Vector3.Dot(transform.forward, parent.transform.forward) > 0)
        {
            GetComponent<Rigidbody>().linearVelocity = transform.forward * serpent.speed / 2;
        } else
        {
            Debug.Log(this.gameObject.name);
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        if (bone != null)
        {
            if (transform.parent == null)
            {
                transform.parent = bone.transform.parent;
                transform.position = bone.transform.position;
                bone.transform.parent = null;
                bone.transform.parent = gameObject.transform;
            }
        }
    }
}
