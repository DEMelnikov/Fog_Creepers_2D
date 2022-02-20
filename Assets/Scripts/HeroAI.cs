using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class HeroAI : MonoBehaviour
{
  //  [SerializeField] private Transform target;
    [SerializeField] private RuntimeAnimatorController _mageAnimateController;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private Vector3 _targetPosition = Vector3.zero;
    private bool _isSelected = false;
    private bool _isMage = false;
    //private Camera _camera;
    //private bool _isWalking = false;
    [SerializeField] private float _actualSpeed;
    //private AnimatorOverrideController _animatonShemeMage;

    //private bool IsSelected => _isSelected;
    private UserInteractions _userInteractions;
    [SerializeField] private Sprite _mageSprite;
    [SerializeField] private CharacterState _state;
    [SerializeField] private float _runeRangeCollision = 1f;
    [SerializeField] private float _runeAwakingSkill = 15f;
    [SerializeField] private float _runeAwakingPower = 5f;
    private RuneData _targetRune;
    private IEnumerator _currentCoroutine;

    //[SerializeField] private Vector3 _velocity; //temp

    //private Vector3 _lastPosition;


    private void Awake()
    {
        //_targetPosition = transform.position;
        //_camera = Camera.main;
        _animator = GetComponent<Animator>();


        //

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        //_targetPoint = null;

        _userInteractions = (UserInteractions)GameObject.FindGameObjectWithTag("UserInteractions")
            .GetComponent("UserInteractions");

        _userInteractions.OnEndDragObjectPositionAction += SetMoveTarget;
        _userInteractions.OnCancelHeroSelection += SetSelection;

        if (_isMage)
        {
            _animator.runtimeAnimatorController = _mageAnimateController;

            _mageSprite = Resources.Load<Sprite>("1");
            GetComponent<SpriteRenderer>().sprite = _mageSprite;
        }
    }

    private void Start()
    {


       // var lastPosition = transform.position;

    }

    public bool IsMage { get => _isMage;}

    void Update()
    {
        switch (_state)
        {
            case CharacterState.Walking:

                Walking();
                break;

            case CharacterState.Idle:
                Idle();
                break;

            case CharacterState.RuneAwaking:
                RuneAwaking();
                break;

            default:
                break;
        }
    }


    public void SetMoveTarget (Vector3 targetPoint)
    {
        _targetPosition = targetPoint;
        if (transform.position != _targetPosition && _isSelected)
        {
            //_isWalking = true;
            _navMeshAgent.isStopped = false;
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_targetPosition);
            _state = CharacterState.Walking;

            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }
    }

    public void SetMage()
    {
        _isMage = true;
    }


    private void OnMouseDown()
    {
        _isSelected = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void RotateHero(Vector3 targetPosition)
    {
        if (targetPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void SetSelection (bool selection)
    {
        _isSelected = selection;
        if (selection==false)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private bool HaveMoveDestination()
    {
        if (_targetPosition == Vector3.zero)
        {
            return false;
        }
        var distance = Vector3.Distance(transform.position, _targetPosition);
        if (distance >= _navMeshAgent.stoppingDistance)
        {
            return true;
        }

        return false;
    }

    private void TryResetMoveDestination()
    {
        if (_targetPosition != Vector3.zero)
        {
            var distance = Vector3.Distance(transform.position, _targetPosition);
            if (distance <= _navMeshAgent.stoppingDistance)
            {
                _targetPosition = Vector3.zero;
                _state = CharacterState.Idle;
            }
        }
    }

    private void Walking()
    {


        var _velocity = _navMeshAgent.velocity;

        _animator.SetFloat("speed", _navMeshAgent.velocity.x);
        if (Mathf.Abs(_navMeshAgent.velocity.x) <= Mathf.Abs(_navMeshAgent.velocity.y))
            _animator.SetFloat("speed", _navMeshAgent.velocity.y);
        RotateHero(_targetPosition);
        TryResetMoveDestination();
    }

    private void Idle()
    {
        Collider2D[] collisionsArray = Physics2D.OverlapCircleAll(transform.position, _runeRangeCollision);

        foreach (var collision in collisionsArray)
        {

            if (collision.TryGetComponent<RuneData>(out RuneData rune) && _isMage && rune.GetAwakingStatus<100f)
            {
                //print("Rune " + rune.AwakingStatus);
                _state = CharacterState.RuneAwaking;
                _targetRune = rune;
            }
            else
            {
                _state = CharacterState.Idle;
            }
        }
    }

    private void RuneAwaking()
    {
        if (_targetRune.IsSleeping)
        {
            if (_currentCoroutine == null)
            {
                _currentCoroutine = AwakeRune(_targetRune.GetAwakingDelay);
                StartCoroutine(_currentCoroutine);
            }
        }
        else
        {
            _state = CharacterState.Idle;
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }
    }

    private IEnumerator AwakeRune (float delay)
    {
        while (_targetRune.IsSleeping && _state == CharacterState.RuneAwaking)
        {
            var check = Random.Range(0, 101 + _targetRune.GetChalengeRating);
            if (check <= _runeAwakingSkill)
            {
                _targetRune.ProgressAwaking(_runeAwakingPower);
            }
            else
            {
                //StopCoroutine(_currentCoroutine);
                //_currentCoroutine = null;
            }

            Debug.Log("Check " + check + "progress " + _targetRune.GetAwakingStatus);
            yield return new WaitForSeconds(delay);
        }
    }
}

enum CharacterState
{
    Walking,
    Idle,
    RuneAwaking
}






