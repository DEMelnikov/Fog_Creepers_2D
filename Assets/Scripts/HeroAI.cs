using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class HeroAI : MonoBehaviour
{
  //  [SerializeField] private Transform target;
    [SerializeField] private Animator _animator;
//    [SerializeField] private float _speed = 2f;

    private NavMeshAgent agent;
    public Vector3 _targetPosition;
    public bool _isSelected = false;
    //private Camera _camera;
    private bool _isWalking = false;
    //public Vector3 _targetPoint;

    public bool IsSelected => _isSelected;

    [SerializeField] private UserInteractions _userInteractions;

    private void Awake()
    {
        //_targetPosition = transform.position;
        //_camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //_targetPoint = null;

        _userInteractions.OnEndDragObjectPositionAction += SetMoveTarget;
        _userInteractions.OnCancelHeroSelection += SetSelection;
    }

    void Update()
    {
        if (_isSelected)
            RotateHero(_targetPosition);

        if (_isWalking)
        {
            _animator.SetFloat("speed", agent.speed);
        }
    }

    public void SetMoveTarget (Vector3 targetPoint)
    {
        _targetPosition = targetPoint;
        if (transform.position != _targetPosition && _isSelected)
        {
            _isWalking = true;
            agent.SetDestination(_targetPosition);
        }
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





