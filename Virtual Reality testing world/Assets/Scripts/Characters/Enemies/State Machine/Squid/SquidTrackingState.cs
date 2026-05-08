using UnityEngine;

public class SquidTrackingState : EnemyTrackingState
{
    Cooldown AttackCooldown;
    float minCooldownTime = 1;
    float maxCooldownTime = 6;
    bool inRange = false;
    public SquidTrackingState(Squid fish, EnemyStateMachine stateMachine) : base(fish, stateMachine) { }
    public override void EnterState()
    {
        fish.agent.stoppingDistance = fish.GetComponent<Squid>().attackRange - 2;
        AttackCooldown = new Cooldown(Random.Range(minCooldownTime, maxCooldownTime));
        AttackCooldown.StartCooldown();
        fish.agent.enabled = true;
        fish.agent.speed = fish.moveSpeed;
        fish.GetComponent<Fish>().animator.SetBool("Swimming", true);
    }
    public override void ExitState()
    {
        fish.GetComponent<Fish>().animator.SetBool("Swimming", false);
    }
    public override void Update()
    {
        base.Update();
        if (Mathf.Abs(Vector3.Distance(fish.target.position, fish.transform.position)) < fish.gameObject.GetComponent<Squid>().attackRange)
        {
            fish.animator.SetBool("InRange", true) ;
            if (!inRange)
            {
                inRange = true;
                AttackCooldown.SetCooldownTime(Random.Range(minCooldownTime, maxCooldownTime));
                AttackCooldown.StartCooldown();
            }
        } else
        {
            fish.animator.SetBool("InRange", false);
            if (inRange)
            {
                inRange = false;
                AttackCooldown.SetCooldownTime(Random.Range(minCooldownTime, maxCooldownTime));
                AttackCooldown.StopCooldown();
            }
        }

        if (!AttackCooldown.isCoolingDown && inRange)
        {
            fish.stateMachine.ChangeState(fish.attackingState);
        }



        
    }
    public override void PhysicsUpdate()
    {
        
    }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerEnter(Collider other)
    {

    }

    public override void OnExitDetectionRadius()
    {
        fish.agent.enabled = false;
        fish.stateMachine.ChangeState(fish.idleState);
    }

    public override void OnEnterAttackRadius()
    {
        fish.agent.stoppingDistance = 0;
        //enemyStateMachine.ChangeState(fish.attackingState);
    }
}
