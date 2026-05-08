using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyTrackingState : EnemyState
{
    public EnemyTrackingState(Fish fish, EnemyStateMachine stateMachine) : base(fish, stateMachine) { }
    public override void EnterState() 
    {
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
        if (fish.active && fish.agent.isOnNavMesh)
        {
            fish.agent.destination = new Vector3(fish.target.position.x, -13, fish.target.position.z);
            fish.targetPosition = fish.target.transform.position;
        } if (!fish.agent.isOnNavMesh && !fish.frozen)
        {
            Vector3 reference = Vector3.zero;
            fish.agent.baseOffset = 0;
            fish.transform.position = Vector3.SmoothDamp(fish.transform.position, Vector3.zero, ref reference, 0.01f);
        }

        
        fish.AlignToTargetDirection();

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
