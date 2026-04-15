using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HarpoonLauncher : Weapon
{
    [Header("Objects")]
    [SerializeField] GameObject harpoon;
    [SerializeField] DistanceJoint3D grapple;
    [SerializeField] LineRenderer cable;

    [Header("Values")]
    [SerializeField] float harpoonTravelSpeed;
    [SerializeField] LayerMask hunterModeLayerMask;
    [SerializeField] LayerMask grappleModeLayerMask;
    [SerializeField] Transform fireTransform;
    [SerializeField] float maxDistance = 20;

    [Header("Inputs")]
    [SerializeField] InputActionReference fireButton;
    [SerializeField] InputActionReference fireModeController;


    bool firing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        fireButton.action.performed += FireHarpoon;
        fireButton.action.canceled += ReleaseHarpoon;
        grapple.ConnectedRigidbody = null;
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //ReelIn();
        cable.SetPosition(0, fireTransform.position);
        cable.SetPosition(1, harpoon.transform.position);
        if (!firing)
        {
            harpoon.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }

    public void FireHarpoon(InputAction.CallbackContext context)
    {
        //RaycastHit hit;

        if (fireModeController.action.IsPressed())
        {
            cable.enabled = true;
            harpoon.transform.parent = null;
            harpoon.GetComponent<Harpoon_Deprecated>().Fire(harpoonTravelSpeed, grappleModeLayerMask, fireTransform, grapple);
            Debug.Log("Fired in Grapple Mode");
            firing = true;
            return;
        }
        Debug.Log("Fired in hunter mode");
    }

    public void ReleaseHarpoon(InputAction.CallbackContext context)
    {
        grapple.ConnectedRigidbody = null;
        harpoon.GetComponent<Harpoon_Deprecated>().Reel();
        //cable.enabled = false;
    }

    public void ReelIn()
    {

    }

    public void AttatchObject(GameObject target)
    {
        throw new System.Exception("This feature is yet to be added");
    }

    public void AttatchPlayer()
    {
        throw new System.Exception("This feature is yet to be added");
    }

    public IEnumerator LaunchAnimation(bool grappleMode, RaycastHit hit)
    {
        yield break;
    }
}
