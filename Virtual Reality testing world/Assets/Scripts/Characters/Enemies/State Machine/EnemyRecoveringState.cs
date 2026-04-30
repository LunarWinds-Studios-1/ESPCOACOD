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
        Vector3 initialRotation = new Vector3(fish.transform.eulerAngles.x, fish.transform.rotation.eulerAngles.y, fish.transform.eulerAngles.z);
        fish.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        float reorientTime = 2;
        float t = 0;
        while (t < reorientTime)
        {
            fish.transform.eulerAngles = Vector3.Lerp(initialRotation, Vector3.zero, t / reorientTime);
            t+= Time.deltaTime;
            yield return null;
        }
        fish.transform.eulerAngles = Vector3.zero;
        fish.stateMachine.ChangeState(fish.idleState);
    }
}
