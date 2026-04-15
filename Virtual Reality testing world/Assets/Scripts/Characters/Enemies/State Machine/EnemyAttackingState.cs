using UnityEngine;

public class EnemyAttackingState : EnemyState
{
    public EnemyAttackingState(Fish fish, EnemyStateMachine stateMachine) : base(fish, stateMachine) { }
    public override void EnterState() 
    {
        fish.agent.enabled = false;
        fish.animator.SetTrigger("Attack");
    }
    public override void ExitState() { }
    public override void Update() 
    {
        
    }
    public override void PhysicsUpdate() { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerEnter(Collider other) 
    {
        
    }

    public override void OnAnimationFinish()
    {
        enemyStateMachine.ChangeState(fish.fleeingState);
    }
}
