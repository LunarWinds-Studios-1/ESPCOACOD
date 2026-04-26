using UnityEngine;

public class Shake : MonoBehaviour
{
    public float intensity = 0;
    [SerializeField] private float speed = 10;

    Vector3 origin;

    Cooldown shakeCoolDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shakeCoolDown = new Cooldown( 1 / speed);
        origin = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shakeCoolDown.isCoolingDown)
        {
            shakeCoolDown.StartCooldown();
            transform.localPosition = Random.insideUnitSphere * intensity;
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
        shakeCoolDown.SetCooldownTime(1 / speed);
    }
}
