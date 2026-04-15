using Unity.VisualScripting;
using UnityEngine;

public class MachineGun : Gun
{
    [SerializeField] float fireRate;
    [SerializeField] float maxFireTime;
    [SerializeField] float rechargeTime;


    [SerializeField] float currentFireTime = 0;
    

    Cooldown rechargeCooldown;
    Cooldown fireCooldown;

    
    public AudioClip rechargeSound;

    AudioSource ambient;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        rechargeCooldown = new Cooldown(rechargeTime);
        fireCooldown = new Cooldown(1 / fireRate);
        ambient = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        ambient.volume = (currentFireTime / maxFireTime);
        bulletSpreadAmount = currentFireTime / maxFireTime;
        if (firing && !rechargeCooldown.isCoolingDown)
        {
            currentFireTime += Time.deltaTime;
            if (!fireCooldown.isCoolingDown) 
            { 
                fireCooldown.StartCooldown();
                
                FireBullet();
                AudioSource.PlayClipAtPoint(fireSound, fireOrigin.position, 0.25f);
            } 
            

            if (currentFireTime >= maxFireTime)
            {
                rechargeCooldown.StartCooldown();
                ReleaseWeapon();
                currentFireTime = 0;
                Debug.Log("COOLING DOWN");
                AudioSource.PlayClipAtPoint(rechargeSound, fireOrigin.position);
            }
        } else if (!firing && !rechargeCooldown.isCoolingDown) 
        {
            currentFireTime -= Time.deltaTime;
        }
        currentFireTime = Mathf.Clamp(currentFireTime, 0, maxFireTime);
    }

    public override void FireBullet()
    {
        base.FireBullet();
    }

    
}
