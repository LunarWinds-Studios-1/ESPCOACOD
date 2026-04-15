using UnityEngine;

public class HarpoonFiringState : HarpoonState
{
    RaycastHit hit;
    float distanceTraveled = 0;
    public HarpoonFiringState(Harpoon harpoon, HarpoonStateMachine stateMachine) : base(harpoon, stateMachine) { }
    
    public override void EnterState()
    {
        base.EnterState();
        harpoon.transform.parent = null;
        distanceTraveled = 0;
        harpoon.rb.linearVelocity = harpoon.origin.forward * harpoon.harpoonTravelSpeed;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        distanceTraveled += Time.fixedDeltaTime * harpoon.harpoonTravelSpeed;
        if (distanceTraveled > harpoon.maxDistance)
        {
            harpoonStateMachine.ChangeState(harpoon.returningState);
        }
        Physics.Raycast(harpoon.transform.position, harpoon.transform.forward, out hit, 0.3f, harpoon.currentLayerMask);

        if (hit.collider != null)
        {
            Debug.Log("Hit something");
            if (hit.collider.gameObject.GetComponent<Fish>() != null)
            {
                Fish fish = hit.collider.gameObject.GetComponent<Fish>();
                if (fish.grabbable)
                {
                    harpoonStateMachine.ChangeState(harpoon.returningState);
                    fish.SetActive(false);
                    fish.stateMachine.ChangeState(fish.grappledState);
                    fish.Damage(5);
                    harpoon.grabbedObject = fish.gameObject;
                    fish.transform.parent = harpoon.transform;
                    fish.transform.position = harpoon.transform.position;
                }
                
                
            } else
            {
                harpoon.rb.linearVelocity = Vector3.zero;
                harpoonStateMachine.ChangeState(harpoon.grapplingState);
            }
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
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if ((harpoon.currentLayerMask.value & (1 << other.transform.gameObject.layer)) <= 0 && other.transform.gameObject.layer != 2)
        {
            harpoonStateMachine.ChangeState(harpoon.returningState);
        }
    }
}
