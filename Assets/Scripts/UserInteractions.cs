using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;

public class UserInteractions : MonoBehaviour
{

    [SerializeField] float _mouseWheelSensitivity = 0.1f;
    //[SerializeField] GameObject test;

    private Camera _camera;
    private Vector3 _dragOrigin;
    private bool _isCameraMove = false;
    public bool _isHeroSelected = false;

    //public delegate Vector3 OnEmptyDrag (Vector3 dragOrigin);

    public delegate void EmptyDrag(Vector3 dragOrigin);
    public delegate void MouseWheel(float scrollDelta);
    public delegate void ObjectDrag(Vector3 dragTarget);
    public delegate void TargetDrag(string gameObjectName);
    public delegate void CancelUserInteraction(bool isSelect);

    public EmptyDrag  OnEmptyStartDragAction; //start drag empty space - mouse not release
    public MouseWheel OnMouseWheelAction; // mouse wheel rotation
    public ObjectDrag OnEndDragObjectPositionAction; //button Up drag from Hero start
    public TargetDrag OnHoldMouseButtonTargetObjecNametAction; //hold mouse button over any object
    public ObjectDrag OnHoldMouseButtonTargetPositionAction; //hold mouse button over target
    public CancelUserInteraction OnCancelHeroSelection; //mouse button Up over obstacles

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
            if (GetHitUnderRaycastMouse(out RaycastHit2D hit2DOrigin))
            {
                string hitName = hit2DOrigin.collider.gameObject.name;
                if (hitName == "Ground" || hitName == "Obstacles" || hitName == "Rune")
                {
                     Debug.Log("Yeah!!!" + hit2DOrigin.collider.gameObject.name);
                    _dragOrigin = _camera.ScreenToWorldPoint(Input.mousePosition);
                    _isCameraMove = true;
                }
                else if (hit2DOrigin.collider.gameObject.name == "Hero")
                {
                    _dragOrigin = hit2DOrigin.collider.gameObject.transform.position;
                    Debug.Log("Target Hero Position: " + hit2DOrigin.collider.gameObject.name);
                    _isHeroSelected = true;
                }
                else
                {
                    Debug.Log("Target Position: " + hit2DOrigin.collider.gameObject.name);
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            GetHitUnderRaycastMouse(out RaycastHit2D hit2D);
            OnHoldMouseButtonTargetObjecNametAction?.Invoke(hit2D.collider.gameObject.name);
            OnHoldMouseButtonTargetPositionAction?.Invoke(GetVector3WithoutZ(_camera.ScreenToWorldPoint(Input.mousePosition)));
        }

        if (Input.GetMouseButton(0) && _isCameraMove)
        {
            OnEmptyStartDragAction?.Invoke(_dragOrigin);           
        }

        if (Input.GetMouseButtonUp(0) && _isHeroSelected)
        {
            GetHitUnderRaycastMouse(out RaycastHit2D hit2D);
            //Debug.Log("Undermouse" + hit2D.collider.name);

            if (hit2D.collider.gameObject.name != "Obstacles")
            {
                OnEndDragObjectPositionAction?.Invoke(GetVector3WithoutZ(_camera.ScreenToWorldPoint(Input.mousePosition)));
                OnHoldMouseButtonTargetObjecNametAction?.Invoke(hit2D.collider.gameObject.name);
            }
            _isHeroSelected = false;
            OnCancelHeroSelection?.Invoke(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ClearSelections();
        }

        if (Input.mouseScrollDelta.y < -_mouseWheelSensitivity || Input.mouseScrollDelta.y > _mouseWheelSensitivity)
            OnMouseWheelAction?.Invoke(Input.mouseScrollDelta.y);
    }

    private bool GetHitUnderRaycastMouse(out RaycastHit2D hit2D)
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

    private void ClearSelections()
    {
        _isCameraMove = false;
        _isHeroSelected = false;
    }

    private Vector3 GetVector3WithoutZ (Vector3 vector3)
    {
        vector3.z = 0;
        return vector3;
    }
}
