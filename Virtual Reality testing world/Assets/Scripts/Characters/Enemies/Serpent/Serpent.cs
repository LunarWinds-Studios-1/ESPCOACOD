using UnityEngine;

public class Serpent : MonoBehaviour
{
    [SerializeField] public Vector3 targetPosition;
    public Vector3 origin;
    Rigidbody rb;

    [SerializeField] public float speed = 5;
    [SerializeField] float travelDistance = 50;
    float waitTime = 5;
    Cooldown waitCooldown;
    bool firstTargetReached = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waitCooldown = new Cooldown(waitTime);
        rb = GetComponent<Rigidbody>();
        waitCooldown.StartCooldown();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AlignToTargetDirection();
        rb.linearVelocity = transform.forward * speed;

        if (Mathf.Abs(Vector3.Distance(targetPosition, transform.position)) < 15 || !waitCooldown.isCoolingDown){

            targetPosition = origin + Random.insideUnitSphere.normalized * travelDistance;
            waitCooldown.StartCooldown();
        }
    }

    public Vector3 GetDirection(Vector3 a, Vector3 b)
    {
        //Vector3 direction = new Vector3(b.x - a.x, b.y - a.y, b.z - a.z).normalized;
        return new Vector3(Mathf.Atan2(b.y - a.y, b.z - a.z), Mathf.Atan2(b.z - a.z, b.x - a.x), 0) * Mathf.Rad2Deg;
    }

    public void AlignToTargetDirection()
    {
        float reference = 0;
        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        var delta = Quaternion.Angle(transform.rotation, targetRotation);

        if (delta > 0)
        {
            var t = Mathf.SmoothDampAngle(delta, 0.0f, ref reference, 0.2f);
            t = 1.0f - t / delta;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
        }

    }


}
