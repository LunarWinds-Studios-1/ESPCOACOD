using UnityEngine;

public class SquidReturningState : EnemyState
{
    Squid squid;
    public SquidReturningState(Squid squid, EnemyStateMachine stateMachine) : base(squid, stateMachine) 
    {
        this.squid = squid;
    }
    public override void EnterState()
    {
        base.EnterState();
        squid.agent.stoppingDistance = squid.attackRange - 2;
    }

    public override void ExitState()
    {
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        fish.AlignToTargetDirection();
        fish.ApproachTargetHeight();
        Vector3 direction = new Vector3(fish.target.position.x - fish.transform.position.x, fish.target.position.y - fish.transform.position.y, fish.target.position.z - fish.transform.position.z);
        if (Mathf.Abs(Vector3.Distance(squid.target.position, squid.transform.position)) < squid.agent.stoppingDistance)
        {
            squid.agent.destination = new Vector3(-direction.x, squid.agent.destination.y, -direction.z);
        }
        else
        {
            squid.stateMachine.ChangeState(squid.trackingState);
        }
    }
}
