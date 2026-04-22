using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] protected float damage;
    [SerializeField] protected LayerMask damageMask;

    public Player player;

    public string name = "Unnamed Weapon";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void ActivateWeapon()
    {

    }

    public virtual void ReleaseWeapon()
    {

    }
}
