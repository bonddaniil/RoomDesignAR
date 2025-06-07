using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FurniturePlacementManager : MonoBehaviour
{
    [Header("UI")]
    public Button[] imageButtons;                       
    public GameObject[] furniturePrefabs;              
    public Sprite[] furnitureIcons;                     

    [Header("AR")]
    public XROrigin xrOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private GameObject SpawnableFurniture;              
    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    private float lastTapTime = 0f;
    private const float doubleTapThreshold = 0.3f;

    void Start()
    {
       
        foreach (var button in imageButtons)
        {
            button.onClick.AddListener(() =>
            {
                Sprite clickedSprite = button.image.sprite;
                GameObject foundPrefab = FindPrefabBySprite(clickedSprite);

                if (foundPrefab != null)
                {
                    SwitchFurniture(foundPrefab);
                }
                else
                {
                    Debug.LogWarning("Prefab not found for sprite: " + clickedSprite.name);
                }
            });
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Time.time - lastTapTime < doubleTapThreshold && isButtonPressed() == false)
            {
                bool collision = raycastManager.Raycast(Input.GetTouch(0).position, raycastHits, TrackableType.PlaneWithinPolygon);
                if (collision && SpawnableFurniture != null)
                {
                    GameObject _object = Instantiate(SpawnableFurniture);
                    _object.transform.position = raycastHits[0].pose.position;
                    _object.transform.rotation = raycastHits[0].pose.rotation;

                    foreach (var plane in planeManager.trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }

                    planeManager.enabled = false;
                }
            }

            lastTapTime = Time.time;
        }
    }

    GameObject FindPrefabBySprite(Sprite sprite)
    {
        for (int i = 0; i < furnitureIcons.Length; i++)
        {
            if (furnitureIcons[i] == sprite)
            {
                return furniturePrefabs[i];
            }
        }

        return null;
    }

    public void SwitchFurniture(GameObject furniture)
    {
        SpawnableFurniture = furniture;
    }

    public bool isButtonPressed()
    {
        return EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() != null;
    }
}