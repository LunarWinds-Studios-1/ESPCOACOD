using UnityEngine;

public class EnemyGrappledState : EnemyState
{
    public EnemyGrappledState(Fish fish, EnemyStateMachine stateMachine) : base(fish, stateMachine) { }

    public override void EnterState()
    {
        fish.agent.enabled = false;
        if (fish.GetComponent<Squid>() != null)
        {
            fish.animator.SetTrigger("Stun");
            fish.animator.SetBool("Stunned", true);
        }

        fish.grappleSource.Play();
    }

    public override void ExitState()
    {
        base.ExitState();
        fish.grappleSource.Stop();
    }
}
