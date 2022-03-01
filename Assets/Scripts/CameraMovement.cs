using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;



public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _zoomStep = 0.25f;
    [SerializeField] private float _minZoomSize = 3f;
    [SerializeField] private float _maxZoomSize = 8f;

    [SerializeField] private float _leftBorder;// = -1f;
    [SerializeField] private float _rightBorder;// = 2f;
    [SerializeField] private float _upperBorder;// = 3f;
    [SerializeField] private float _bottomBorder;// = -5f;

    private UserInteractions _userInteractions;
    private Camera _camera;

    //public UserInteractions UserInteractions; //{ get => _userInteractions; set => _userInteractions = value; }

    private void Awake()
    {
        _camera = Camera.main;
        _userInteractions = (UserInteractions)GameObject.FindGameObjectWithTag("UserInteractions")
            .GetComponent("UserInteractions");

        _userInteractions.OnEmptyStartDragAction += Move;
        _userInteractions.OnMouseWheelAction += Zoom;

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

    public void Zoom(float scrollDelta)
    {
        float newSize = _camera.orthographicSize + _zoomStep;
        if (scrollDelta > 0)
            newSize = _camera.orthographicSize - _zoomStep;


        _camera.orthographicSize = Mathf.Clamp(newSize, _minZoomSize, _maxZoomSize);
    }

    //private void OnEmptyDragActionHandler
}

