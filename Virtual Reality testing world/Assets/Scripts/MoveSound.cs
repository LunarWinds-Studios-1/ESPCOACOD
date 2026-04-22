using UnityEngine;

public class MoveSound : MonoBehaviour
{
    Vector3 targetAngle;
    Cooldown moveTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveTimer = new Cooldown(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveTimer.isCoolingDown)
        {
            targetAngle = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            moveTimer.StartCooldown();
        }
        Vector3 reference = Vector3.zero;
        transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, targetAngle, ref reference, 1);
    }
}
