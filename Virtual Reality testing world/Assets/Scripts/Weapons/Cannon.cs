using UnityEngine;

[RequireComponent (typeof(Shake))]
public class Cannon : Gun
{
    [SerializeField] GameObject cannonball;
    
    [SerializeField] LineRenderer trajectory;
    [SerializeField] int points = 100;

    [SerializeField] float chargeTime = 2;
    [SerializeField] float minimumVelocity = 5;
    [SerializeField] float maximumVelocity = 15;
    [SerializeField] float shakeIntensity = 0.25f;
    float maxTrajectoryDistance = 1;
    float projectileVelocity;
    float time = 0;
    Shake shake;
    public override void FireBullet()
    {
        AudioSource.PlayClipAtPoint(fireSound, fireOrigin.position);
        GameObject cb = Instantiate(cannonball, fireOrigin.position, Quaternion.identity);
        Rigidbody rb = cb.GetComponent<Rigidbody>();
        CannonBall cannonBall = cb.GetComponent<CannonBall>();
        cannonBall.damageMask = damageMask;
        cannonBall.damage = damage;
        rb.linearVelocity = fireOrigin.forward * projectileVelocity;
        Debug.Log("Actual Velocities: X: " + new Vector2(rb.linearVelocity.x, rb.linearVelocity.z).magnitude + " Y: " + rb.linearVelocity.y);

    }
    public override void Start()
    {
        base.Start();
        shake = GetComponent<Shake>();
    }
    public override void ActivateWeapon()
    {
        firing = true;
    }

    public override void ReleaseWeapon()
    {
        base.ReleaseWeapon();
        FireBullet();
    }

    public override void Update()
    {
        base.Update();
        if (firing)
        {
            projectileVelocity = Mathf.Lerp(minimumVelocity, maximumVelocity, time/ chargeTime);
            shake.intensity = Mathf.Lerp(0, shakeIntensity * 0.1f, time/ chargeTime);
            if (time < chargeTime)
            {
                time += Time.deltaTime;
            } else
            {
                time = chargeTime;
            }
        } else
        {
            time = 0;
            shake.intensity = 0;
        }
        DrawAimTrajectory();
        trajectory.enabled = firing;
    }

    public void DrawAimTrajectory()
    {
        float gravity = Physics.gravity.y;
        
        trajectory.positionCount = points;
        float yVel = -projectileVelocity * Mathf.Sin(fireOrigin.eulerAngles.x * Mathf.Deg2Rad);
        float xVel = projectileVelocity * Mathf.Cos(fireOrigin.eulerAngles.x * Mathf.Deg2Rad);
        Debug.Log("Projected Velocities: X: " + xVel + " Y: " + yVel);
        for (int i = 0; i < points; i++)
        {
            float deltaT = maxTrajectoryDistance / (float)points;
            deltaT *= i;
            float height;
            height = fireOrigin.position.y + yVel * deltaT + 0.5f * gravity * Mathf.Pow(deltaT, 2);
            float straight;
            straight = deltaT * xVel;
            float localZVel = Mathf.Cos(fireOrigin.eulerAngles.y * Mathf.Deg2Rad);
            float localXVel = Mathf.Sin(fireOrigin.eulerAngles.y * Mathf.Deg2Rad);
            trajectory.SetPosition(i, new Vector3(fireOrigin.position.x + straight * localXVel, height, fireOrigin.position.z + straight * localZVel));
        }
    }
}
