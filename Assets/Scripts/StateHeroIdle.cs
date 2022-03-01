using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHeroIdle : State
{
    private Animator _animator;
    private Hero _hero;

    public StateHeroIdle (Hero hero)
    {
        _hero = hero;
    }

    public override void Enter()
    {
        base.Enter();
        _animator = _hero.gameObject.GetComponent<Animator>();
        _animator.SetFloat("speed", 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
