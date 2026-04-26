using UnityEngine;

public interface IDamageable 
{


    void Damage(float damage);
    void Damage(float damage, Vector3 hitPosition);
    void Die();
    void Knockback(Vector3 direction, float strength);
}
