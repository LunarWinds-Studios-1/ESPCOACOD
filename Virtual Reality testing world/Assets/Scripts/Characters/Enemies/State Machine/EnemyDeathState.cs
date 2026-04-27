using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(Fish fish, EnemyStateMachine stateMachine) : base(fish, stateMachine)
    {

    }

    public override void EnterState()
    {
        fish.animator.SetTrigger("Death");
    }
    public override void ExitState()
    {
        
    }
    public override void PhysicsUpdate() { }
    public override void Update() { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnEnterDetectionRadius()
    {

    }
}
