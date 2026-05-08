using System.Collections.Generic;
using UnityEngine;

public class Squid : Fish
{
    [SerializeField] public float attackRange = 5;
    public SquidReturningState returningState;
    public List<string> attacks = new List<string>();
    [SerializeField] public ParticleSystem particles;
    public override void InstantiateStates()
    {
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine);
        trackingState = new SquidTrackingState(this, stateMachine);
        attackingState = new SquidAttackingState(this, stateMachine);
        fleeingState = new EnemyFleeingState(this, stateMachine);
        grappledState = new EnemyGrappledState(this, stateMachine);
        recoveringState = new EnemyRecoveringState(this, stateMachine);
        deathState = new EnemyDeathState(this, stateMachine);
        returningState = new SquidReturningState(this, stateMachine);
    }

    public override void Damage(float damage, Vector3 point)
    {
        if (damage > maxHealth / 2)
        {
            stateMachine.ChangeState(fleeingState);
        }
        base.Damage(damage, point);
    }
}
