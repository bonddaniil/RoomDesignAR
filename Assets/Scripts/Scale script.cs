using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    public float minScale = 0.1f;
    public float maxScale = 3.0f;
    public float scaleSpeed = 0.01f;

    private float previousDistance;
    private bool isScaling = false;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Ray ray0 = Camera.main.ScreenPointToRay(touchZero.position);
            Ray ray1 = Camera.main.ScreenPointToRay(touchOne.position);

            RaycastHit hit0, hit1;

            bool isTouchingObject0 = Physics.Raycast(ray0, out hit0);
            bool isTouchingObject1 = Physics.Raycast(ray1, out hit1);

            
            if (isTouchingObject0 && isTouchingObject1 &&
                hit0.transform.root == hit1.transform.root)
            {
                GameObject targetRoot = hit0.transform.root.gameObject;

                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                if (!isScaling)
                {
                    previousDistance = currentDistance;
                    isScaling = true;
                }
                else
                {
                    float distanceDelta = currentDistance - previousDistance;
                    float scaleFactor = 1 + (distanceDelta * scaleSpeed);

                    Vector3 newScale = targetRoot.transform.localScale * scaleFactor;
                    newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
                    newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
                    newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);

                    targetRoot.transform.localScale = newScale;
                    previousDistance = currentDistance;
                }
            }
            else
            {
                isScaling = false;
            }
        }
        else
        {
            isScaling = false;
        }
    }
}
