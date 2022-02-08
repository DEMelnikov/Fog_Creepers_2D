using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField]
    private GameObject _owner;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private LineRenderer lineRenderer;    

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _owner.GetComponent<SpriteRenderer>().color == Color.green)
        {
            lineRenderer.enabled = true;
        }

        if (Input.GetMouseButton(0))
        {
            _startPosition = transform.parent.position;

            _endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _endPosition.z = 0;

            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;

            lineRenderer.SetPosition(0, _startPosition);
            lineRenderer.SetPosition(1, _endPosition);

        }

        if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
        }
    }
}