using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{

    [SerializeField] InputActionProperty triggerValue;
    [SerializeField] InputActionProperty gripValue;

    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float trigger = triggerValue.action.ReadValue<float>();
        float grip = gripValue.action.ReadValue<float>();

        animator.SetFloat("Trigger", trigger);
        animator.SetFloat("Grip", grip);
    }
}
