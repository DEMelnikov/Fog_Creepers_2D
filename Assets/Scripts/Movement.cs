using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 _targetPosition;
    private bool _isSelected = false;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (_targetPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0) && _isSelected)
        {
            _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _targetPosition.z = transform.position.z;
            _isSelected = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        _isSelected = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }
}
