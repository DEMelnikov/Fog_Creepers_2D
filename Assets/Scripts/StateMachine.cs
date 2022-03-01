using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine   
{
    public State CureentState { get; set; }

    public void Initialize (State startState)
    {
        CureentState = startState;
        CureentState.Enter();
    }

    public void ChangeState (State newState)
    {
        CureentState.Exit();
        CureentState = newState;
        CureentState.Enter();
    }
}
