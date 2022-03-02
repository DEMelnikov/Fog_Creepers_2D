using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract HeroStates StateName { get; }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void Update()
    {

    }
}

public enum HeroStates
{
    Idle,
    Walking,
    RuneAwaking
}


