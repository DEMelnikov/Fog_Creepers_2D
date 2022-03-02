using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHeroRuneAwaking : State
{
    private Animator _animator;
    private Hero _hero;
    private IEnumerator _currentCoroutine;

    public override HeroStates StateName => HeroStates.RuneAwaking;

    public StateHeroRuneAwaking(Hero hero)
    {
        _hero = hero;
    }

    public  override void Enter()
    {
        base.Enter();
        _animator = _hero.gameObject.GetComponent<Animator>();
        _animator.SetFloat("speed", 0);
       
        
    }

    public override void Exit()
    {
        base.Exit();
        _hero.RemoveTargetRune();

        if (_currentCoroutine != null)
        {
            _hero.StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }

    public override void Update()
    {
        base.Update();
        Awaking();
    }

    private void Awaking ()
    {
        if (_hero.TargetRune.IsSleeping)
        {
            if (_currentCoroutine == null)
            {
                _currentCoroutine = AwakeRune(_hero.TargetRune.GetAwakingDelay);
                _hero.StartCoroutine(_currentCoroutine);
            }
        }
        else
        {
            _hero.ResetStateToIdle();
        }
    }

    private IEnumerator AwakeRune(float delay)
    {
        while (_hero.TargetRune.IsSleeping)
        {
            //Debug.Log("QQQ");
            var check = Random.Range(0, 101 + _hero.TargetRune.GetChalengeRating);
            if (check <= _hero.RuneAwakingSkill)
            {
                _hero.TargetRune.ProgressAwaking(_hero.RuneAwakingPower);
            }
            yield return new WaitForSeconds(delay);
        }
    }

}
