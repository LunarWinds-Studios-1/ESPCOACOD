using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(DistanceJoint3D))]
public class Harpoon : MonoBehaviour
{
    HarpoonStateMachine stateMachine;

    public Rigidbody playerRB;

    public HarpoonIdleState idleState { get; set; }
    public HarpoonReturningState returningState { get; set; }
    public HarpoonFiringState firingState { get; set; }
    public HarpoonGrapplingState grapplingState { get; set; }

    public Transform origin;


    public bool canFire = true;
    public bool grappleMode = false;

    [HideInInspector] public Rigidbody rb;

    public float harpoonTravelSpeed = 10;
    public float maxDistance = 25;

    [SerializeField] LayerMask grappleModeMask;
    [SerializeField] LayerMask huntModeMask;
    [HideInInspector] public LayerMask currentLayerMask;

    public DistanceJoint3D joint;
    public float reelSpeed = 5;

    [HideInInspector] public GameObject grabbedObject;

    [Header("Aim Indicator")]
    [SerializeField] Color validTargetColor;
    [SerializeField] Color invalidTargetColor;
    RaycastHit aimIndicatorHit;
    [SerializeField] LayerMask targetableLayerMask;
    [SerializeField] GameObject indicator;

    private void Awake()
    {
        stateMachine = new HarpoonStateMachine();
        idleState = new HarpoonIdleState(this, stateMachine);
        returningState = new HarpoonReturningState(this, stateMachine);
        firingState = new HarpoonFiringState(this, stateMachine);
        grapplingState = new HarpoonGrapplingState(this, stateMachine);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<DistanceJoint3D>();
        stateMachine.Initialize(idleState);
        
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
        UpdateAimIndicator();
    }
    
    public void Fire()
    {
        
        stateMachine.CurrentState.OnFire();
    }


    public void UpdateAimIndicator()
    {
        Physics.Raycast(transform.position, transform.forward, out aimIndicatorHit, maxDistance, targetableLayerMask);
        if (grappleMode)
        {
            currentLayerMask = grappleModeMask;
        }
        else
        {
            currentLayerMask = huntModeMask;
        }
        if (aimIndicatorHit.collider != null)
        {
            indicator.transform.position = aimIndicatorHit.point;
            if ((currentLayerMask.value & (1 << aimIndicatorHit.collider.transform.gameObject.layer)) > 0)
            {
                indicator.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", validTargetColor);
            } else
            {
                indicator.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", invalidTargetColor);
            }
        }
    }


    public void Release()
    {
        stateMachine.CurrentState.OnRelease();
    }

    private void OnTriggerStay(Collider other)
    {
        stateMachine.CurrentState.OnTriggerStay(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        stateMachine.CurrentState.OnTriggerEnter(other);
    }
}
