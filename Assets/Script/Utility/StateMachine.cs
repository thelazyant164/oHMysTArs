using UnityEngine;

public abstract class FiniteStateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    public void SetState(State newState)
    {
        CurrentState?.Terminate();
        CurrentState = newState;
        StartCoroutine(CurrentState.Start());
    }
}
