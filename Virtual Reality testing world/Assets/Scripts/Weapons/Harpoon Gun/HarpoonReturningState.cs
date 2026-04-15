using UnityEngine;

public class HarpoonReturningState : HarpoonState
{
    public HarpoonReturningState(Harpoon harpoon, HarpoonStateMachine stateMachine) : base(harpoon, stateMachine) { }
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        harpoon.rb.linearVelocity = new Vector3(harpoon.origin.position.x - harpoon.transform.position.x, harpoon.origin.position.y - harpoon.transform.position.y, harpoon.origin.position.z - harpoon.transform.position.z).normalized * harpoon.harpoonTravelSpeed;
    }

    public override void OnFire()
    {
        base.OnFire();
    }

    public override void OnRelease()
    {
        base.OnRelease();
    }

    public override void ExitState()
    {
        base.ExitState();
        
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if (other.gameObject.tag == "Harpoon")
        {
            Debug.Log(other.gameObject.name);
            harpoonStateMachine.ChangeState(harpoon.idleState);
        }
    }
}
