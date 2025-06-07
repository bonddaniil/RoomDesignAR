using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageCarousel : MonoBehaviour
{
    public Button nextButton; 
    public Button previousButton; 
    public Image[] imageButtons; 
    public List<Sprite> imageList; 

    private int currentStartIndex = 0; 

    void Start()
    {
       
        nextButton.onClick.AddListener(ShowNextImages);
        previousButton.onClick.AddListener(ShowPreviousImages);

       
        UpdateImages();
    }

    void UpdateImages()
    {
       
        for (int i = 0; i < imageButtons.Length; i++)
        {
            int index = currentStartIndex + i;

            if (index < imageList.Count)
            {
                imageButtons[i].sprite = imageList[index]; 
                imageButtons[i].gameObject.SetActive(true); 
            }
            else
            {
                imageButtons[i].gameObject.SetActive(false); 
            }
        }

        previousButton.interactable = currentStartIndex > 0;
        nextButton.interactable = currentStartIndex + imageButtons.Length < imageList.Count;
    }

    void ShowNextImages()
    {
       
        if (currentStartIndex + imageButtons.Length < imageList.Count)
        {
            currentStartIndex += imageButtons.Length;
            UpdateImages();
        }
    }

    void ShowPreviousImages()
    {
       
        if (currentStartIndex - imageButtons.Length >= 0)
        {
            currentStartIndex -= imageButtons.Length;
            UpdateImages();
        }
    }
}

