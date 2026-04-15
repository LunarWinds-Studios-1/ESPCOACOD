using System;
using UnityEngine;

public class HarpoonIdleState : HarpoonState
{
    public HarpoonIdleState(Harpoon harpoon, HarpoonStateMachine stateMachine) : base(harpoon, stateMachine) { }


    public override void EnterState()
    {
        base.EnterState();
        harpoon.canFire = true;
        harpoon.transform.parent = harpoon.origin;
        harpoon.transform.position = harpoon.origin.position;
        harpoon.transform.rotation = harpoon.origin.rotation;
        harpoon.joint.enabled = false;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        harpoon.rb.linearVelocity = harpoon.playerRB.linearVelocity;
        harpoon.transform.position = harpoon.origin.position;
        harpoon.transform.rotation = harpoon.origin.rotation;
    }

    public override void OnFire()
    {
        base.OnFire();
        harpoonStateMachine.ChangeState(harpoon.firingState);
        harpoon.canFire = false;
    }

    public override void OnRelease()
    {
        base.OnRelease();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
