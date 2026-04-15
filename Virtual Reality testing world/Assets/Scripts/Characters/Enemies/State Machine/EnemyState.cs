using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine enemyStateMachine;
    protected Fish fish;


    public EnemyState(Fish fish, EnemyStateMachine enemyStateMachine)
    {
        this.fish = fish;
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }

    public virtual void Update() { }
    public virtual void PhysicsUpdate() { }
    public virtual void OnTriggerExit(Collider other) { }
    public virtual void OnTriggerStay(Collider other) { }
    public virtual void OnTriggerEnter(Collider other) { }
    public virtual void OnEnterDetectionRadius() { }
    public virtual void OnExitDetectionRadius() { }
    public virtual void OnEnterAttackRadius() { }
    public virtual void OnExitAttackRadius() { }
    public virtual void OnAnimationFinish()
    {

    }
}
