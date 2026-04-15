using UnityEngine;

public class FishAnimationHandler : MonoBehaviour
{
    [SerializeField] Fish fish;

    public void OnAnimationFinish()
    {
        fish.OnAnimationFinish();
        Debug.Log("ATTACK FINISHED!");
    }
}
