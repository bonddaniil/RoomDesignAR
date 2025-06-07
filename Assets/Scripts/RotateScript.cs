using UnityEngine;

public class SimpleObjectRotator : MonoBehaviour
{
   
    public float rotationSpeed = 0.2f;

    private GameObject selectedObject;

    void Update()
    {
        
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
          
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
    
                    selectedObject = hit.transform.root.gameObject;
                }
            }
            else if (touch.phase == TouchPhase.Moved && selectedObject != null)
            {
            
                float deltaX = touch.deltaPosition.x;
                selectedObject.transform.Rotate(Vector3.up, -deltaX * rotationSpeed, Space.World);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                
                selectedObject = null;
            }
        }
    }
}
