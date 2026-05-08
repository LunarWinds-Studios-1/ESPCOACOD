using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] LayerMask damageMask;
    public int damage;  
    GameManager manager;
    private void Start()
    {
        manager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((damageMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            other?.GetComponent<IDamageable>()?.Damage((float) damage * manager.globalDifficulty);
            if (GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
