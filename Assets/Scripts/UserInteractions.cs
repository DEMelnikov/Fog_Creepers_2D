using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;

public class UserInteractions : MonoBehaviour
{

    [SerializeField] float _mouseWheelSensitivity = 0.1f;

    private Camera _camera;
    private Vector3 _dragOrigin;
    private bool _isCameraMove = false;

    //public delegate Vector3 OnEmptyDrag (Vector3 dragOrigin);

    public delegate void EmptyDrag(Vector3 dragOrigin);
    public delegate void MouseWheel(float scrollDelta);

    public EmptyDrag OnEmptyDrag;
    public MouseWheel OnMouseWheelAction;
        
    //public event OnEmptyDrag onEmptyDragAction;

    //public event EventHandler<EventOptionsObject> OnEmptyDragAction;

    //public UnityEvent
    private void Awake()

    {
        _camera = Camera.main;

        //OnEmptyDrag onEmptyDragAction;

        //EmptyDragAction?.Invoke(new Vector3(0, 0, 0));
    }

        void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (GetHitUnderRaycastMouse(out RaycastHit2D hit2D))
            {
                if (hit2D.collider.gameObject.name == "Ground" || hit2D.collider.gameObject.name == "Obstacles")
                {
                    Debug.Log("Yeah!!!" + hit2D.collider.gameObject.name);
                    _dragOrigin = _camera.ScreenToWorldPoint(Input.mousePosition);
                    _isCameraMove = true;
                }
                else
                {
                    Debug.Log("Target Position: " + hit2D.collider.gameObject.name);
                }
            }
        }

        if (Input.GetMouseButton(0) && _isCameraMove)
        {
            //CameraMovement();
            //_camera.GetComponent<CameraMovement>().Move(_dragOrigin);

            //onEmptyDragAction?.Invoke(_dragOrigin );

            OnEmptyDrag?.Invoke(_dragOrigin);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isCameraMove = false;
        }

        Debug.Log("Wheel " + Input.mouseScrollDelta.y);



        if (Input.mouseScrollDelta.y < -_mouseWheelSensitivity || Input.mouseScrollDelta.y >_mouseWheelSensitivity)
            OnMouseWheelAction?.Invoke(Input.mouseScrollDelta.y);



    }

    private bool  GetHitUnderRaycastMouse(out RaycastHit2D hit2D)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        hit2D = Physics2D.GetRayIntersection(ray);

        if (hit2D.collider != null)
        {
            //Debug.Log("Target Position: " + hit2D.collider.gameObject.name);
            return true;
        }
        return false;
    }
}
