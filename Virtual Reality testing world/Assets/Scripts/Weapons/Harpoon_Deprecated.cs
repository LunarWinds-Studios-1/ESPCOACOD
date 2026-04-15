using UnityEngine;

public class Harpoon_Deprecated : MonoBehaviour
{

    RaycastHit hit;
    LayerMask mask;
    Vector3 direction;
    float velocity;
    Transform origin;
    Rigidbody rb;
    DistanceJoint3D joint;

    [SerializeField] float reelSpeed = 5;
    [SerializeField] Rigidbody playerRB;

    bool returning = false;
    bool firing = false;

    bool grappling = false;
    bool attatched;

    Vector3 moveVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<DistanceJoint3D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Physics.Raycast(transform.position, direction, out hit, 0.5f, mask);

        if (hit.collider != null && !returning && !attatched)
        {
            rb.linearVelocity = Vector3.zero;
            transform.position = hit.point;

            attatched = true;
            if (grappling)
            {
                joint.enabled = true;
                joint.distance = Vector3.Distance(transform.position, playerRB.position);
                joint.ConnectedRigidbody = playerRB;
                moveVector = new Vector3(transform.position.x - playerRB.position.x, transform.position.y - playerRB.position.y, transform.position.z - playerRB.position.z);
            }
        }

        if (grappling)
        {
            joint.distance -= reelSpeed * Time.fixedDeltaTime;
        }

        if (returning)
        {
            joint.enabled = false;
            GetComponent<Rigidbody>().linearVelocity = new Vector3(origin.position.x - transform.position.x, origin.position.y - transform.position.y, origin.position.z - transform.position.z) * velocity;
        }
        if (!returning && !firing)
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            transform.position = origin.position;
            transform.rotation = origin.rotation;
        }
    }

    public void Fire(float velocity, LayerMask mask, Transform origin, bool grapple)
    {
        if (!firing)
        {
            this.mask = mask;
            this.direction = origin.forward;
            this.velocity = velocity;
            this.origin = origin;
            grappling = grapple;
            firing = true;
            GetComponent<Rigidbody>().linearVelocity = direction * velocity;
        }
        
    }


    public void Reel()
    {
        returning = true;
        attatched = false;
        if (grappling)
        {
            playerRB.linearVelocity = moveVector.normalized * reelSpeed;
            grappling = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Harpoon")
        {
            returning = false;
            firing = false;
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            transform.position = origin.position;
            transform.rotation = origin.rotation;
            transform.parent = origin.gameObject.transform;
        }
    }
}
