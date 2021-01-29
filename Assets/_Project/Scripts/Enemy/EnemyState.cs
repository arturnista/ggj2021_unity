using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    
    public delegate void ChangeStateHandler(State newState, State lastState);
    public event ChangeStateHandler OnChangeState;

    public enum State
    {
        Idle,
        Scream,
        Moving,
        Knockout,
        Attacking,
        StopAttacking,
        Dying
    }

    private State _state;
    public State CurrentState => _state;

    private void Awake()
    {
        _state = State.Idle;
    }

    public void ChangeState(State newState)
    {
        State lastState = _state;
        _state = newState;

        OnChangeState?.Invoke(_state, lastState);
    }

}
