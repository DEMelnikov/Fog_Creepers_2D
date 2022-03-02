using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class StateHeroWalk : State
{
    private Hero _hero;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Vector3 _previousTargetPoint;

    public override HeroStates StateName { get; } = HeroStates.Walking;

    public StateHeroWalk(Hero hero)
    {
        _hero = hero;
    }

    public override void Enter()
    {
        base.Enter();
        _navMeshAgent = _hero.gameObject.GetComponent<NavMeshAgent>();
        _navMeshAgent.isStopped = false;
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_hero.GetTargetPoint);

        _animator = _hero.gameObject.GetComponent<Animator>();
    }

    public override void Exit()
    {
        base.Exit();
        RotateHero(_previousTargetPoint);
    }

    public override void Update()
    {
        base.Update();
        Walking();
    }

    private void Walking()
    {
        _animator.SetFloat("speed", Mathf.Abs(_navMeshAgent.velocity.x));
        if (Mathf.Abs(_navMeshAgent.velocity.x) <= Mathf.Abs(_navMeshAgent.velocity.y))
            _animator.SetFloat("speed", Mathf.Abs(_navMeshAgent.velocity.y));

        RotateHero(_hero.GetTargetPoint);
        TryResetMoveDestination();
    }

    private void RotateHero(Vector3 targetPoint)
    {
        if (targetPoint.x < _hero.gameObject.transform.position.x)
        {
            _hero.gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            _hero.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void TryResetMoveDestination()
    {
        if (_hero.GetTargetPoint != Vector3.zero)
        {
            var distance = Vector3.Distance(_hero.gameObject.transform.position, _hero.GetTargetPoint);
            if (distance <= _navMeshAgent.stoppingDistance)
            {
                _previousTargetPoint = _hero.GetTargetPoint;
                _hero.ResetTargetpoint();
                _hero.ResetStateToIdle();
            }
        }
    }
}
