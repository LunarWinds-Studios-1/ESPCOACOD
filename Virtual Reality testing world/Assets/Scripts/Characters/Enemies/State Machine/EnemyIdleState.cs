using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Fish fish, EnemyStateMachine stateMachine) : base(fish, stateMachine)
    {

    }

    public override void EnterState() { }
    public override void ExitState() { }
    public override void PhysicsUpdate() { }
    public override void Update() { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnEnterDetectionRadius()
    {
        enemyStateMachine.ChangeState(fish.trackingState);
    }
}
