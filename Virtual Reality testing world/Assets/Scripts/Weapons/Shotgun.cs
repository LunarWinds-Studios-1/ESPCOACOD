using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] float reloadTime = 2;
    [SerializeField] int bulletsPerShot = 6;
    [SerializeField] int shotsPerLoad = 2;

    private int shotsRemaining;
    bool loaded = true;

    Cooldown reloadCooldown;

    public override void Start()
    {
        base.Start();
        name = "Minnow Shotgun";
        shotsRemaining = shotsPerLoad;
        reloadCooldown = new Cooldown(reloadTime);
    }

    public override void Update()
    {
        base.Update();
        if (!reloadCooldown.isCoolingDown && !loaded)
        {
            loaded = true;
            shotsRemaining = shotsPerLoad;
        }
    }

    public override void ActivateWeapon()
    {
        if (loaded)
        {
            base.ActivateWeapon();
            for (int i = 0; i < bulletsPerShot; i++)
            {
                bulletSpreadAmount = Random.Range(0, 2);
                FireBullet();
            }

            AudioSource.PlayClipAtPoint(fireSound, fireOrigin.position);

            shotsRemaining--;

            if (shotsRemaining <= 0)
            {
                reloadCooldown.StartCooldown();
                loaded = false;
            }
        }
    }
}
