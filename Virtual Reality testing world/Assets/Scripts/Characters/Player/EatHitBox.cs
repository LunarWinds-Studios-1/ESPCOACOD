using UnityEngine;

public class EatHitBox : MonoBehaviour
{
    public bool triggered;
    public LayerMask eatLayerMask;
    [HideInInspector] public GameObject obj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerStay(Collider other)
    {
        if ((eatLayerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            obj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((eatLayerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            obj = null;
        }
    }
}
