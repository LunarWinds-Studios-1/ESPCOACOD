using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] LayerMask damageMask;
    public int damage;  

    private void OnTriggerEnter(Collider other)
    {
        if ((damageMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            other?.GetComponent<IDamageable>()?.Damage(damage);
        }
    }
}
