using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteractions : MonoBehaviour
{
    [SerializeField] float _mouseWheelSensitivity = 0.1f;

    private Camera _camera;
    private Vector3 _dragOrigin;
    private bool _isCameraMove = false;


    private void Awake()
    {
        _camera = Camera.main;
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
            _camera.GetComponent<CameraMovement>().Move(_dragOrigin);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isCameraMove = false;
        }

        if (Input.mouseScrollDelta.y > _mouseWheelSensitivity)
        {
            _camera.GetComponent<CameraMovement>().ZoomIn();
        }

        if (Input.mouseScrollDelta.y < -_mouseWheelSensitivity)
        {
            _camera.GetComponent<CameraMovement>().ZoomOut();
        }




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
