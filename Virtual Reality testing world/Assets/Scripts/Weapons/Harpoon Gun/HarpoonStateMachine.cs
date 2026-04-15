using UnityEngine;

public class HarpoonStateMachine
{
    public HarpoonState CurrentState { get; set; }

    public void Initialize(HarpoonState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }

    public void ChangeState(HarpoonState newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}
