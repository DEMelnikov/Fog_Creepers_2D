using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hero : MonoBehaviour
{
    [SerializeField] private bool _isMage = false;
    [SerializeField] private Vector3 _targetPoint = Vector3.zero;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private UserInteractions _userInteractions;
    private bool _isSelected = false;

    private StateMachine _stateMachine;
    private StateHeroIdle _stateIdle;

    public bool IsMage { get => _isMage; }
    public Vector3 GetTargetPoint { get => _targetPoint; }

    private void Awake()
    {
        _stateIdle = new StateHeroIdle(this);

        _stateMachine = new StateMachine();
        _stateMachine.Initialize(_stateIdle);

        _animator = GetComponent<Animator>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        _userInteractions = (UserInteractions)GameObject.FindGameObjectWithTag("UserInteractions")
            .GetComponent("UserInteractions");

        _userInteractions.OnCancelHeroSelection += SetSelection;
        _userInteractions.OnEndDragObjectPositionAction += SetMoveTarget;
    }

    public void SetMage() => _isMage = true;

    public void ResetTargetpoint () => _targetPoint = Vector3.zero;

    public void ResetStateToIdle() => _stateMachine.ChangeState(_stateIdle);

    private void Start()
    {
        if (_isMage)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("1");
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("AnimateControllers/Mage/Mage");
        }
    }

    private void Update()
    {
        _stateMachine.CureentState.Update();
    }

    private void SetSelection(bool selection)
    {
        _isSelected = selection;
        if (selection == false)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        _isSelected = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void SetMoveTarget(Vector3 targetPoint)
    {
        _targetPoint = targetPoint;
        if (transform.position != _targetPoint && _isSelected)
            _stateMachine.ChangeState(new StateHeroWalk(this));
    }
}


