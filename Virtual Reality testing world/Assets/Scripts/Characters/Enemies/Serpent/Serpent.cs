using UnityEngine;

public class Serpent : MonoBehaviour
{
    [SerializeField] Vector3 targetPosition;
    Rigidbody rb;

    [SerializeField] public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AlignToTargetDirection();
        rb.linearVelocity = transform.forward * speed;

        if (Mathf.Abs(Vector3.Distance(targetPosition, transform.position)) < 2){
            targetPosition = Random.insideUnitSphere * 50;
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
            var t = Mathf.SmoothDampAngle(delta, 0.0f, ref reference, 0.1f);
            t = 1.0f - t / delta;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t);
        }
        //Debug.Log(targetRotation);
        //transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, targetRotation, ref reference, 0.1f);
    }


}
