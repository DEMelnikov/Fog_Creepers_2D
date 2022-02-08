using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 _targetPosition;
    private bool _isSelected = false;
    private Camera _camera;

    public Animator Animator;

    //private BoxCollider2D boxCollider;

    public bool IsSelected => _isSelected;

    // Start is called before the first frame update

    private void Awake()
    {
        _targetPosition = transform.position;
        _camera = Camera.main;
    }

    void Update()
    {

        if (_targetPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
            Animator.SetFloat("speed", 1f);
        }
        else
        {
            Animator.SetFloat("speed", 0f);
        }

        if (Input.GetMouseButtonUp(0) && _isSelected)
        {
            _targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _targetPosition.z = transform.position.z;
            _isSelected = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            RotateHero(_targetPosition);
        }
    }


    private void OnMouseDown()
    {
        _isSelected = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void RotateHero (Vector3 targetPosition)
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
