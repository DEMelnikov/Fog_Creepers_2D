using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class HeroAI : MonoBehaviour
{
  //  [SerializeField] private Transform target;
//    [SerializeField] private float _speed = 2f;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    public Vector3 _targetPosition;
    private bool _isSelected = false;
    public bool _isMage = false;
    //private Camera _camera;
    private bool _isWalking = false;

    //private bool IsSelected => _isSelected;
    private UserInteractions _userInteractions;

    
    private void Awake()
    {
        //_targetPosition = transform.position;
        //_camera = Camera.main;
        _animator = GetComponent<Animator>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        //_targetPoint = null;

        _userInteractions = (UserInteractions)GameObject.FindGameObjectWithTag("UserInteractions")
            .GetComponent("UserInteractions");

        _userInteractions.OnEndDragObjectPositionAction += SetMoveTarget;
        _userInteractions.OnCancelHeroSelection += SetSelection;
    }


    public bool IsMage { get => _isMage;}

    void Update()
    {
        //if (_isSelected || transform.position != _targetPosition)


        if (_isWalking)
        {
            _animator.SetFloat("speed", _navMeshAgent.speed);
            RotateHero(_targetPosition);
        }

    }

    public void SetMoveTarget (Vector3 targetPoint)
    {
        _targetPosition = targetPoint;
        if (transform.position != _targetPosition && _isSelected)
        {
            _isWalking = true;
            _navMeshAgent.isStopped = false;
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_targetPosition);

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
}





