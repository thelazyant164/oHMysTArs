using UnityEngine;

public abstract class FiniteStateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    public void SetState(State newState)
    {
        if (CurrentState != null) 
        {
            StopCoroutine(CurrentState.Start()); 
            CurrentState.Terminate();
        }
        CurrentState = newState;
        StartCoroutine(CurrentState.Start());
    }
}
