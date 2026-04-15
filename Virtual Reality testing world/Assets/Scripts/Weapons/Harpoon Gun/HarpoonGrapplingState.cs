using UnityEngine;

public class HarpoonGrapplingState : HarpoonState
{
    float currentReelSpeed;
    float reelAcceleration = 10;
    public HarpoonGrapplingState(Harpoon harpoon, HarpoonStateMachine stateMachine) : base(harpoon, stateMachine) { }
    public override void EnterState()
    {
        base.EnterState();
        currentReelSpeed = 0;
        harpoon.joint.enabled = true;
        harpoon.joint.distance = Mathf.Abs(Vector3.Distance(harpoon.transform.position, harpoon.playerRB.position));
        harpoon.joint.ConnectedRigidbody = harpoon.playerRB;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        harpoon.joint.distance -= currentReelSpeed * Time.deltaTime;
        if (currentReelSpeed < harpoon.reelSpeed)
        {
            currentReelSpeed += reelAcceleration * Time.fixedDeltaTime;
        }

        if (harpoon.joint.distance < 4)
        {
            harpoonStateMachine.ChangeState(harpoon.returningState);
        }
    }

    public override void OnFire()
    {
        base.OnFire();
    }

    public override void OnRelease()
    {
        base.OnRelease();
        harpoonStateMachine.ChangeState(harpoon.returningState);
    }

    public override void ExitState()
    {
        base.ExitState();
        harpoon.joint.enabled = false;
        harpoon.playerRB.linearVelocity = new Vector3(harpoon.transform.position.x - harpoon.playerRB.transform.position.x, harpoon.transform.position.y - harpoon.playerRB.transform.position.y, harpoon.transform.position.z - harpoon.playerRB.transform.position.z).normalized * currentReelSpeed;
        Debug.Log(harpoon.playerRB.linearVelocity.magnitude);
    }
}
