using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //private Transform _position;
    //private float _speed = 2f;
    [SerializeField] private float _zoomStep = 0.25f;
    [SerializeField] private float _minZoomSize = 3f;
    [SerializeField] private float _maxZoomSize = 8f;

    [SerializeField] private float _leftBorder;// = -1f;
    [SerializeField] private float _rightBorder;// = 2f;
    [SerializeField] private float _upperBorder;// = 3f;
    [SerializeField] private float _bottomBorder;// = -3f;

    private Camera _camera;    

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _leftBorder = - 1;
        _rightBorder = _camera.orthographicSize / 2 + 1;
        _upperBorder = _camera.orthographicSize / 2 + 1;
        _bottomBorder =  - 1;

        _camera.transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, _leftBorder,_rightBorder),
            Mathf.Clamp(transform.position.y, _bottomBorder, _upperBorder),
            transform.position.z
            );
    }

    public void Move(Vector3 originPosition)
    {
        Vector3 positionsDelta = originPosition - _camera.ScreenToWorldPoint(Input.mousePosition);
        _camera.transform.position += positionsDelta;
    }

    public void ZoomIn()
    {
        float newSize = _camera.orthographicSize + _zoomStep;
        _camera.orthographicSize = Mathf.Clamp(newSize, _minZoomSize, _maxZoomSize);
    }

    public void ZoomOut()
    {
        float newSize = _camera.orthographicSize - _zoomStep;
        _camera.orthographicSize = Mathf.Clamp(newSize, _minZoomSize, _maxZoomSize);
    }
}
