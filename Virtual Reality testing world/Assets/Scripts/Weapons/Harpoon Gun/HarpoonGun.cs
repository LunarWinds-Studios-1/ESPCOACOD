using UnityEngine;
using UnityEngine.InputSystem;

public class HarpoonGun : MonoBehaviour
{
    [SerializeField] InputActionReference trigger;
    [SerializeField] InputActionReference primaryButton;

    [HideInInspector] public GameObject currentHarpoon;

    [SerializeField] Harpoon harpoon;
    [SerializeField] Harpoon plunger;

    [SerializeField] Animator animator;

    LineRenderer lr;
    bool firing  = false;
    bool transitioning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHarpoon = harpoon.gameObject;
        trigger.action.performed += OnFire;
        trigger.action.canceled += OnRelease;
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, currentHarpoon.GetComponent<Harpoon>().origin.position);
        lr.SetPosition(1, currentHarpoon.transform.position);
        if (!firing)
        {
            currentHarpoon.GetComponent<Harpoon>().grappleMode = primaryButton.action.IsPressed();
            if (currentHarpoon.GetComponent<Harpoon>().grappleMode && currentHarpoon.gameObject != plunger.gameObject)
            {
                currentHarpoon = plunger.gameObject;

                animator.SetTrigger("ToPlunge");

            }
            else if (!currentHarpoon.GetComponent<Harpoon>().grappleMode && currentHarpoon.gameObject != harpoon.gameObject)
            {
                currentHarpoon = harpoon.gameObject;
                animator.SetTrigger("ToHarpoon");
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!transitioning)
        {
            firing = true;
            currentHarpoon.GetComponent<Harpoon>().Fire();
        }
    }

    public void OnRelease(InputAction.CallbackContext context)
    {
        currentHarpoon.GetComponent<Harpoon>().Release();
        Release();
        firing = false;
    }

    public void Release()
    {
        Harpoon h = currentHarpoon.GetComponent<Harpoon>();
        if (h.grabbedObject != null)
        {
            if (h.grabbedObject.GetComponent<Fish>() != null)
            {
                h.grabbedObject.GetComponent<Fish>().SetActive(true);
                h.grabbedObject.GetComponent<Fish>().stateMachine.ChangeState(h.grabbedObject.GetComponent<Fish>().fleeingState);
                h.grabbedObject.transform.parent = null;
                h.grabbedObject = null;
            }
        }
    }

    public void OnAnimationStart()
    {
        transitioning = true;
    }

    public void OnAnimationEnd()
    {
        transitioning = false;
    }
}
