using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyTrackingState : EnemyState
{
    public EnemyTrackingState(Fish fish, EnemyStateMachine stateMachine) : base(fish, stateMachine) { }
    public override void EnterState() 
    {
        fish.agent.enabled = true;
        fish.agent.speed = fish.moveSpeed;
        fish.GetComponent<Animator>().SetBool("Swimming", true);
    }
    public override void ExitState() 
    {
        fish.GetComponent<Animator>().SetBool("Swimming", false);
    }
    public override void Update() 
    {
        if (fish.active)
        {
            fish.agent.destination = new Vector3(fish.target.position.x, -13, fish.target.position.z);
        }
        /*if (active)
        {
            agent.destination = new Vector3(target.position.x, 0, target.position.z);

            cooldownTimer += Time.deltaTime;
            if (cooldownTimer > cooldownTime)
            {
                cooldownTimer = 0;
                cooldownTime = UnityEngine.Random.Range(0.5f, 10);
                randomHeightOffset = UnityEngine.Random.Range(-1, 1);
            }

        } */
        fish.ApproachTargetHeight();
    }
    public override void PhysicsUpdate() 
    {
        ;
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
        enemyStateMachine.ChangeState(fish.attackingState);
    }
}
