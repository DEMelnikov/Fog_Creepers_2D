using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class HeroAI : MonoBehaviour
{
  //  [SerializeField] private Transform target;
  //  [SerializeField] private Animator _animator;
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
        //RaycastHit2D hit;



        //RaycastHit2D hit = ;

        //if (hit.collider != null)
        //{
        //    Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
        //}
        //else
        //{
        //    print("Hit!!!");
        //}

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        Debug.Log("Target Position: " + hit.collider.gameObject.name);

        //RaycastHit2D hit;

        //Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, out hit);

        //if (Input.GetMouseButton(0) )
        ////if (Input.GetMouseButton(0) && Physics.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition))\
        //{
        //    Debug.Log("qqq");
        //    //Debug.Log("Target Position: " + hit.collider.gameObject.name);
        //}


        if (Input.GetMouseButtonUp(0) && _isSelected)
        {
            _targetPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            _targetPoint.z = transform.position.z;
            _isSelected = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            RotateHero(_targetPosition);
            _isWalking = true;
        }

        if (_isWalking)
        {
            MoveToTarget(_targetPoint);
            //_animator.SetFloat("speed", agent.speed);
        }


    }

    private void OnMouseDown()
    {
        _isSelected = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        //Debug.Log("click");
        //print("!!");
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





