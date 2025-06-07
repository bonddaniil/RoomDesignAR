using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectDragger : MonoBehaviour
{
    public float holdTimeThreshold = 0.3f; 
    private float touchStartTime;
    private bool isDragging = false;

    private GameObject selectedObject;

    private ARRaycastManager raycastManager;
    private Camera arCamera;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
        arCamera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

           
            if (touch.phase == TouchPhase.Began)
            {
                touchStartTime = Time.time;
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                   
                    selectedObject = hit.transform.root.gameObject;
                }
            }

           
            else if (touch.phase == TouchPhase.Stationary && selectedObject != null)
            {
                if (!isDragging && Time.time - touchStartTime >= holdTimeThreshold)
                {
                    isDragging = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging && selectedObject != null)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    selectedObject.transform.position = hitPose.position;
                }
            }

        
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
                selectedObject = null;
            }
        }
    }
}
