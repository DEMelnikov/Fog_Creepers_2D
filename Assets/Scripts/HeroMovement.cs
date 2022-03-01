using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPosition = Vector3.zero;

    private Animator _animator;
    private bool _isSelected = false;
    private NavMeshAgent _navMeshAgent;
    private UserInteractions _userInteractions;
    private IEnumerator _currentCoroutine;

    private void Awake()
    {
        _userInteractions = (UserInteractions)GameObject.FindGameObjectWithTag("UserInteractions")
    .GetComponent("UserInteractions");

        //_userInteractions.OnEndDragObjectPositionAction += SetMoveTarget;
        //_userInteractions.OnCancelHeroSelection += SetSelection;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    //public void Walking()
    //{
    //    var _velocity = _navMeshAgent.velocity;

    //    _animator.SetFloat("speed", Mathf.Abs(_navMeshAgent.velocity.x));
    //    if (Mathf.Abs(_navMeshAgent.velocity.x) <= Mathf.Abs(_navMeshAgent.velocity.y))
    //        _animator.SetFloat("speed", Mathf.Abs(_navMeshAgent.velocity.y));

    //    RotateHero(_targetPosition);
    //    TryResetMoveDestination();
    //}

    //private void SetSelection(bool selection)
    //{
    //    _isSelected = selection;
    //    if (selection == false)
    //    {
    //        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    _isSelected = true;
    //    gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    //}

    //private void SetMoveTarget(Vector3 targetPoint)
    //{
    //    _targetPosition = targetPoint;
    //    if (transform.position != _targetPosition && _isSelected)
    //    {
    //        _navMeshAgent.isStopped = false;
    //        _navMeshAgent.enabled = true;
    //        _navMeshAgent.SetDestination(_targetPosition);

    //        gameObject.GetComponent<HeroAI>().SetCharacterState(CharacterState.Walking);

    //        if (gameObject.GetComponent<HeroAI>().CurrentCoroutine != null)
    //        {
    //            StopCoroutine(_currentCoroutine);
    //            gameObject.GetComponent<HeroAI>().StopCoroutine();
    //        }
    //    }
    //}

    //private void RotateHero(Vector3 targetPosition)
    //{
    //    if (targetPosition.x < transform.position.x)
    //    {
    //        transform.localScale = new Vector3(-1f, 1f, 1f);
    //    }
    //    else
    //    {
    //        transform.localScale = new Vector3(1f, 1f, 1f);
    //    }
    //}

    //private bool HaveMoveDestination()
    //{
    //    if (_targetPosition == Vector3.zero)
    //    {
    //        return false;
    //    }
    //    var distance = Vector3.Distance(transform.position, _targetPosition);
    //    if (distance >= _navMeshAgent.stoppingDistance)
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    //private void TryResetMoveDestination()
    //{
    //    if (_targetPosition != Vector3.zero)
    //    {
    //        var distance = Vector3.Distance(transform.position, _targetPosition);
    //        if (distance <= _navMeshAgent.stoppingDistance)
    //        {
    //            _targetPosition = Vector3.zero;
    //            gameObject.GetComponent<HeroAI>().SetCharacterState(CharacterState.Idle);
    //            //_animator.SetFloat("speed",0);
    //        }
    //    }
    //}
}
