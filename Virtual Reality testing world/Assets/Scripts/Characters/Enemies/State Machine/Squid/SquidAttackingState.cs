using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
public class SquidAttackingState : EnemyAttackingState
{
    List<string> attacks = new List<string>();
    Squid squid;
    bool attackTriggered = false;
    public SquidAttackingState(Fish fish, EnemyStateMachine stateMachine) : base(fish, stateMachine) 
    {
        squid = fish.GetComponent<Squid>();
    }
    public override void EnterState()
    {
        attacks = squid.attacks;
        fish.agent.stoppingDistance = .8f;
        squid.agent.speed = squid.moveSpeed * 2;
        attackTriggered = false;
    }
    public override void ExitState() 
    {
        squid.agent.speed = squid.moveSpeed;
        squid.agent.enabled = true;
    }
    
    public override void Update()
    {
        if (fish.active && fish.agent.isOnNavMesh)
        {
            fish.agent.destination = new Vector3(fish.target.position.x, -13, fish.target.position.z);
        }
        if (!fish.agent.isOnNavMesh && !fish.frozen)
        {
            Vector3 reference = Vector3.zero;
            fish.agent.baseOffset = 0;
            fish.transform.position = Vector3.SmoothDamp(fish.transform.position, Vector3.zero, ref reference, 0.01f);
        }
        if (!fish.frozen)
        {
            fish.targetPosition = fish.target.transform.position; 
        }
        fish.AlignToTargetDirection();
        fish.ApproachTargetHeight();
    }
    public override void PhysicsUpdate() { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerEnter(Collider other)
    {

    }
    public override void OnEnterAttackRadius()
    {
        base.OnEnterAttackRadius();
        //squid.agent.enabled = false;
        if (!attackTriggered)
        {
            squid.animator.SetTrigger(attacks[Random.Range(0, attacks.Count)]);
            attackTriggered = true;
        }
    }
    public override void OnAnimationFinish()
    {
        enemyStateMachine.ChangeState(squid.returningState);
    }

    public override void OnSoundEvent()
    {
        base.OnSoundEvent();
        squid.audioSource.clip = squid.attackSounds[Random.Range(0, squid.attackSounds.Count)];
        squid.audioSource.Play();
    }
}
