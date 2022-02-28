using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class HeroAI : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController _mageAnimateController;

    [SerializeField] private bool _isMage = false;
    [SerializeField] private CharacterState _state;
    [SerializeField] private float _runeAwakingSkill = 15f;
    [SerializeField] private float _runeAwakingPower = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private float _actualSpeed;
    [SerializeField] private float _skillStealth = 65f;
    //[SerializeField] private float _runeRangeCollision = 1f;

    private Component _movement;
    private Sprite _mageSprite;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

    private UserInteractions _userInteractions;
    private RuneData _targetRune;
    private IEnumerator _currentCoroutine;

    public IEnumerator CurrentCoroutine { get => _currentCoroutine; }

    private float _skillStealthDelay = 2f;

    private void Awake()
    {
        _movement = gameObject.GetComponent<HeroMovement>();
        //_targetPosition = transform.position;
        //_camera = Camera.main;
        _animator = GetComponent<Animator>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        //_targetPoint = null;

        _userInteractions = (UserInteractions)GameObject.FindGameObjectWithTag("UserInteractions")
            .GetComponent("UserInteractions");

        //_userInteractions.OnEndDragObjectPositionAction += SetMoveTarget;
        //_userInteractions.OnCancelHeroSelection += SetSelection;

        _state = CharacterState.Idle;
    }

    private void Start()
    {

        if (_isMage)
        {
            _animator.runtimeAnimatorController = _mageAnimateController;

            _mageSprite = Resources.Load<Sprite>("1");
            GetComponent<SpriteRenderer>().sprite = _mageSprite;
        }
    }

    public bool IsMage { get => _isMage;}

    private void Update()
    {
        switch (_state)
        {
            case CharacterState.Walking:
                //_movement.Walking();
                gameObject.GetComponent<HeroMovement>().Walking();
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

    public void StopCoroutine ()
    {
        _currentCoroutine = null;
    }

    public void SetMage()
    {
        _isMage = true;
    }

    public void SetCharacterState (CharacterState state)
    {
        _state = state;
    }

    private void Idle()
    {
        _animator.SetFloat("speed", 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.name == "Rune" && _isMage && collisionObject.GetComponent<RuneData>().IsSleeping)
        {
            if (_state == CharacterState.Idle)
            {
                _state = CharacterState.RuneAwaking;
                _targetRune = collisionObject.GetComponent<RuneData>();
            }

            if (_state == CharacterState.RuneAwaking)
                _state = CharacterState.RuneAwaking;
        }
    }

    private void RuneAwaking()
    {
        _animator.SetFloat("speed", 0);

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

    private IEnumerator AwakeRune(float delay)
    {
        while (_targetRune.IsSleeping && _state == CharacterState.RuneAwaking)
        {
            var check = Random.Range(0, 101 + _targetRune.GetChalengeRating);
            if (check <= _runeAwakingSkill)
            {
                _targetRune.ProgressAwaking(_runeAwakingPower);
            }
            yield return new WaitForSeconds(delay);
        }
    }
}

public enum CharacterState
{
    Walking,
    Idle,
    RuneAwaking
}






