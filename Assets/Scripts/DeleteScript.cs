using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    public float doubleTapTime = 0.3f;

    private float lastTapTime = 0f;
    private GameObject lastTappedObject;

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
                    GameObject tappedObject = hit.transform.gameObject;

                    if (lastTappedObject == tappedObject && Time.time - lastTapTime < doubleTapTime)
                    {
                        Destroy(tappedObject.transform.root.gameObject);
                    }

                    lastTapTime = Time.time;
                    lastTappedObject = tappedObject;
                }
            }
        }
    }
}
