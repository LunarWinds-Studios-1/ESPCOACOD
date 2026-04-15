using UnityEngine;

public class EnemyGrappledState : EnemyState
{
    public EnemyGrappledState(Fish fish, EnemyStateMachine stateMachine) : base(fish, stateMachine) { }

    public override void EnterState()
    {
        fish.agent.enabled = false;
    }
}
