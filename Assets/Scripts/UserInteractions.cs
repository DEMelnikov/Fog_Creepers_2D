using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteractions : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

        void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("!!!");
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            //Ray ray = _camera.ScreenPointToRay(controls);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.name);
            }
        }


    }
}
