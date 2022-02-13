using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private GameObject _owner;
    [SerializeField] private UserInteractions _userInteractions;

    private Vector3 _startPosition;
    [SerializeField]  private Vector3 _endPosition;
    private LineRenderer lineRenderer;
    private Camera _camera;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        _camera = Camera.main;
        _userInteractions.OnHoldMouseButtonTargetObjecNametAction += ChangeColor;
        _userInteractions.OnHoldMouseButtonTargetPositionAction += SetEndPosition;
    }

    void Update()
    {
        if (_owner.GetComponent<SpriteRenderer>().color == Color.green)
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
            ChangeColor("white");
        }

        if (lineRenderer.enabled)
        {
            _startPosition = transform.parent.position;

            _endPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _endPosition.z = 0;

            //ChangeColor("white");


            lineRenderer.SetPosition(0, _startPosition);
            lineRenderer.SetPosition(1, _endPosition);

        }

        if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
        }
    }

    private void ChangeColor (string name)
    {
        switch (name)
        {
            case "Obstacles":
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;

                break;

            case "Ground":
                lineRenderer.startColor = Color.white;
                lineRenderer.endColor = Color.green;

                break;

            default:
                lineRenderer.startColor = Color.white;
                lineRenderer.endColor = Color.white;
                break;
        }
    }

    private void SetEndPosition (Vector3 position)
    {
        _endPosition = position;
    }
}