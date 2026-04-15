using UnityEngine;

public class DetectionSphere : MonoBehaviour
{
    [SerializeField] Fish fish;

    private void Start()
    {
        GetComponent<SphereCollider>().radius = fish.aggroRadius;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((fish.targetableLayerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            
            fish.OnEnterDetectionRadius();
            fish.target = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((fish.targetableLayerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            fish.OnExitDetectionRadius();
            fish.target = null;
        }
    }
}
