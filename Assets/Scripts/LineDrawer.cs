using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public Transform StartPosition;
    private Vector3 _endPosition;
    private LineRenderer lineRenderer;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lineRenderer.enabled = true;
        }

        if (Input.GetMouseButton(0))
        {
            _endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _endPosition.z = 0;

            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;

            lineRenderer.SetPosition(0, StartPosition.position);
            lineRenderer.SetPosition(1, _endPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
        }


    }
}