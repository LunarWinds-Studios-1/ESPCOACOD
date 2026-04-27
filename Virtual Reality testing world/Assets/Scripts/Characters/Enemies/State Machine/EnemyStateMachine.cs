using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState CurrentState { get; set; }
    public bool locked = false;
    public void Initialize(EnemyState startingState)
    {
        CurrentState = startingState;
        CurrentState.EnterState();
    }

    public void ChangeState(EnemyState newState)
    {
        if (!locked)
        {
            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }
    }
}
