using UnityEngine;

public class HarpoonState
{
    protected Harpoon harpoon;
    protected HarpoonStateMachine harpoonStateMachine;

    public HarpoonState(Harpoon harpoon, HarpoonStateMachine harpoonStateMachine)
    {
        this.harpoon = harpoon;
        this.harpoonStateMachine = harpoonStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void PhysicsUpdate() { }
    public virtual void OnFire() { }
    public virtual void OnRelease() { }
    public virtual void OnTriggerStay(Collider other) { }
    public virtual void OnTriggerEnter(Collider other) { }
}
