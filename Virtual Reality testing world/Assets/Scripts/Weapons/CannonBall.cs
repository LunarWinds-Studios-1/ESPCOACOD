using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject decal;

    public LayerMask damageMask;
    public float damage;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        //exp.GetComponent<Explosion>().damageMask = damageMask;
        exp.GetComponent<Explosion>().damage = damage / 2;
        if ((damageMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            collision.gameObject?.GetComponent<IDamageable>().Damage(damage);
        }
        //Instantiate(decal, transform.position, Quaternion.Euler(collision.GetContact(0).normal));
        Destroy(gameObject);
    }
}
