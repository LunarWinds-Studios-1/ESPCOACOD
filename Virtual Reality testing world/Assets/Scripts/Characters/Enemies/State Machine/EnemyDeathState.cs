using UnityEngine;

public class EnemyDeathState : EnemyState
{
    ParticleSystem particles;
    public EnemyDeathState(Fish fish, EnemyStateMachine stateMachine) : base(fish, stateMachine)
    {

    }

    public override void EnterState()
    {
        fish.animator.SetTrigger("Death");
        fish.audioSource.clip = fish.death[Random.Range(0, fish.death.Count)];
        fish.audioSource.Play();

        if (fish.GetComponent<Squid>() != null)
        {
            particles = fish.GetComponent<Squid>().particles;

            particles.Play();
        }
    }
    public override void ExitState()
    {
        

    }

    public override void OnAnimationFinish()
    {
        base.OnAnimationFinish();
        particles.Stop();
    }
    public override void PhysicsUpdate() { }
    public override void Update() { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnEnterDetectionRadius()
    {

    }
}
