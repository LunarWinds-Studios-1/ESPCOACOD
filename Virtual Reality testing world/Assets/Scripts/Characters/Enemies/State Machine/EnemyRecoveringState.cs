using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRecoveringState : EnemyState
{
    public EnemyRecoveringState(Fish fish, EnemyStateMachine stateMachine) : base(fish, stateMachine)
    {

    }

    public override void EnterState() 
    {
        fish.StartCoroutine(Reorient());
    }
    public override void ExitState() 
    {
        fish.ConstrainRigidbody();
    }
    public override void PhysicsUpdate() { }
    public override void Update() { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnEnterDetectionRadius()
    {
        
    }

    public IEnumerator Reorient()
    {
        yield return new WaitForSeconds(2);
        fish.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        Vector3 reference = Vector3.zero;
        while (Mathf.Abs(Vector3.Distance(fish.transform.eulerAngles, Vector3.zero)) > 0.5f)
        {
            fish.transform.eulerAngles = Vector3.SmoothDamp(fish.transform.eulerAngles, Vector3.zero, ref reference, 0.1f);
            yield return null;
        }
        fish.transform.eulerAngles = Vector3.zero;
        fish.stateMachine.ChangeState(fish.idleState);
    }
}
