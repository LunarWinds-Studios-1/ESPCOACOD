
using Unity.VisualScripting;
using UnityEngine;


public class Gun : Weapon
{
    [SerializeField] GameObject bulletHolePrefab;

    [SerializeField] protected float range = 50;
    [SerializeField] protected Transform fireOrigin;
    public AudioClip fireSound;
    protected bool firing = false;
    protected float bulletSpreadAmount = 1;
    public virtual void FireBullet()
    {
        RaycastHit hit;
        Vector2 bulletSpread = GetTrajectoryOffset(bulletSpreadAmount);
        Vector3 fireDirection = new Vector3(fireOrigin.forward.x + bulletSpread.x, fireOrigin.forward.y + bulletSpread.y, fireOrigin.forward.z);
        Physics.Raycast(fireOrigin.position, fireDirection, out hit, range, damageMask);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<IDamageable>() != null)
            {
                IDamageable enemy = hit.collider.gameObject.GetComponent<IDamageable>();
                enemy.Damage(damage, hit.point);
            } else
            {
                GameObject tempBulletEffect = Instantiate(bulletHolePrefab, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)) as GameObject;
                Destroy(tempBulletEffect, 10.0f);
            }
            
        }
        Debug.Log("PEW!");
    }

    public Vector3 GetTrajectoryOffset(float bulletSpreadAmount)
    {
        Vector2 spreadAmount = Random.insideUnitCircle * bulletSpreadAmount * 0.1f;
        return spreadAmount;
    }

    public override void ActivateWeapon()
    {
        base.ActivateWeapon();
        firing = true;
    }

    public override void ReleaseWeapon()
    {
        base.ReleaseWeapon();
        firing = false;
    }
}
