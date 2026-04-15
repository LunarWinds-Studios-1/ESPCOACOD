using UnityEngine;
using UnityEngine.InputSystem;

public class HarpoonGun : MonoBehaviour
{
    [SerializeField] InputActionReference trigger;
    [SerializeField] InputActionReference primaryButton;

    [SerializeField] Harpoon harpoon;

    LineRenderer lr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trigger.action.performed += OnFire;
        trigger.action.canceled += OnRelease;
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, harpoon.origin.position);
        lr.SetPosition(1, harpoon.transform.position);
        harpoon.grappleMode = primaryButton.action.IsPressed();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        
        harpoon.Fire();
    }

    public void OnRelease(InputAction.CallbackContext context)
    {
        harpoon.Release();

        if (harpoon.grabbedObject != null)
        {
            if (harpoon.grabbedObject.GetComponent<Fish>() != null)
            {
                harpoon.grabbedObject.GetComponent<Fish>().SetActive(true);
                harpoon.grabbedObject.GetComponent<Fish>().stateMachine.ChangeState(harpoon.grabbedObject.GetComponent<Fish>().fleeingState);
                harpoon.grabbedObject.transform.parent = null;
                harpoon.grabbedObject = null;
            }
        }
    }
}
