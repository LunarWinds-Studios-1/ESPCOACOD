using UnityEngine;
using UnityEngine.InputSystem;

public class Cutlass : Weapon
{

    
    [SerializeField] InputActionReference swordVelocityReference;
    float speed;

    [SerializeField] float minimumSpeed = 0.5f;
    
    private void OnTriggerEnter(Collider other)
    {
        if ((damageMask.value & (1 << other.transform.gameObject.layer)) <= 0)
        {
            speed = Mathf.Abs(swordVelocityReference.action.ReadValue<Vector3>().magnitude + player.rb.linearVelocity.magnitude);
            if (speed > minimumSpeed)
            {
                other.gameObject?.GetComponent<IDamageable>()?.Damage(damage * speed);
            }
        }
    }
}
