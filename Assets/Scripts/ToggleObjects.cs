using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle3DObjects : MonoBehaviour
{
    public List<GameObject> objectList; 
    public Button nextButton; 
    public Button prevButton; 

    private int currentIndex = 0; // ������ ��������� ��'����

    void Start()
    {
        
        if (objectList == null || objectList.Count == 0)
        {
            Debug.LogError("objectList �� ������� ��� �������!");
            return;
        }

      
        HideAllObjects();

        
        objectList[currentIndex].SetActive(true);

     
        if (nextButton != null)
            nextButton.onClick.AddListener(ShowNextObject);

        if (prevButton != null)
            prevButton.onClick.AddListener(ShowPreviousObject);
    }

    private void HideAllObjects()
    {
        
        foreach (var obj in objectList)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    private void ShowNextObject()
    {
        
        objectList[currentIndex].SetActive(false);

        currentIndex = (currentIndex + 1) % objectList.Count;

        objectList[currentIndex].SetActive(true);
    }

    private void ShowPreviousObject()
    {
        
        objectList[currentIndex].SetActive(false);

        
        currentIndex = (currentIndex - 1 + objectList.Count) % objectList.Count;

        objectList[currentIndex].SetActive(true);
    }
}

