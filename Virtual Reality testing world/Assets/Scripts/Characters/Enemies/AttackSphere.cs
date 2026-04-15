using UnityEngine;

public class AttackSphere : MonoBehaviour
{
    [SerializeField] Fish fish;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = fish.attackRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((fish.targetableLayerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            fish.OnEnterAttackRadius();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((fish.targetableLayerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            fish.OnExitAttackRadius();
        }
    }
}
