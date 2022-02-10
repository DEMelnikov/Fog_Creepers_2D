using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroAI : MonoBehaviour
{
  //  [SerializeField] private Transform target;
    [SerializeField] private Animator _animator;
//    [SerializeField] private float _speed = 2f;

    private NavMeshAgent agent;
    private Vector3 _targetPosition;
    private bool _isSelected = false;
    private Camera _camera;
    private bool _isWalking = false;
    private Vector3 _targetPoint;

    public bool IsSelected => _isSelected;

    private void Awake()
    {
        //_targetPosition = transform.position;
        _camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //_targetPoint = null;
    }


    void Start()
    {

    }

    void Update()
    {
        // _targetPosition = target.position;
        //MoveToTarget(_targetPosition);

        if (Input.GetMouseButtonUp(0) && _isSelected)
        {
            _targetPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            _targetPoint.z = transform.position.z;
            _isSelected = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            RotateHero(_targetPosition);
            _isWalking = true;
            print(_targetPoint.x);
            
        }

        if (_isWalking)
        {
            MoveToTarget(_targetPoint);
            _animator.SetFloat("speed", agent.speed);
        }


    }

    private void OnMouseDown()
    {
        _isSelected = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("click");
        print("!!");
    }

    private void MoveToTarget(Vector3 _targetPoint)
    {
        agent.SetDestination(_targetPoint);
        
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
}
