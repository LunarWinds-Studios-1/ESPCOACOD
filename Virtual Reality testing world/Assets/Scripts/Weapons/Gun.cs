
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class Gun : Weapon
{
    [SerializeField] GameObject bulletHolePrefab;
    [SerializeField] TrailRenderer bulletTrail;
    [SerializeField] protected float range = 50;
    [SerializeField] protected Transform fireOrigin;
    [SerializeField] protected float bulletTravelTime = 0.1f;
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
            TrailRenderer trail = Instantiate(bulletTrail, fireOrigin.position, Quaternion.identity);
            StartCoroutine(BulletTrail(trail, hit));
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

    public IEnumerator BulletTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.gameObject.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / bulletTravelTime;

            yield return null;
        }

        trail.transform.position = hit.point;

        Destroy(trail, trail.time);
    }
}
