using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GalleryScroll : MonoBehaviour
{
    [Header("UI Elements")]
    public Image[] displaySlots; 
    public Button nextButton;
    public Button prevButton;
    public TMP_Dropdown categoryDropdown;

    [Header("Data")]
    public List<Sprite> allImages;
    public List<Sprite> chairImages;
    public List<Sprite> tableImages;
    public List<Sprite> sofaImages;
    public List<Sprite> otherImages;
    public List<Sprite> comfortImages;
    public List<Sprite> ergonomicsImages;
    public List<Sprite> universalityImages;

    private Dictionary<string, List<Sprite>> imageCategories;
    private List<Sprite> currentImageList;
    private int currentIndex = 0;

    void Start()
    {
       
        nextButton.onClick.AddListener(Next);
        prevButton.onClick.AddListener(Previous);
        categoryDropdown.onValueChanged.AddListener(OnCategoryChanged);

      
        imageCategories = new Dictionary<string, List<Sprite>>()
        {
            { "All", allImages },
            { "Chairs", chairImages },
            { "Tables", tableImages },
            { "Sofas", sofaImages },
            { "Other", otherImages },
            { "Comfort", comfortImages },
            { "Ergonomics", ergonomicsImages },
            { "Universality", universalityImages }
        };

    
        currentImageList = allImages;
        UpdateGallery();
    }

    void OnCategoryChanged(int index)
    {
        string selected = categoryDropdown.options[index].text;
        if (imageCategories.ContainsKey(selected))
        {
            currentImageList = imageCategories[selected];
            currentIndex = 0;
            UpdateGallery();
        }
        else
        {
            Debug.LogWarning("No category found for: " + selected);
        }
    }

    void Next()
    {
        if (currentImageList.Count == 0) return;
        currentIndex = (currentIndex + 1) % currentImageList.Count;
        UpdateGallery();
    }

    void Previous()
    {
        if (currentImageList.Count == 0) return;
        currentIndex = (currentIndex - 1 + currentImageList.Count) % currentImageList.Count;
        UpdateGallery();
    }

    void UpdateGallery()
    {
        for (int i = 0; i < displaySlots.Length; i++)
        {
            if (currentImageList.Count == 0)
            {
                displaySlots[i].sprite = null;
                continue;
            }

            int index = (currentIndex + i) % currentImageList.Count;
            displaySlots[i].sprite = currentImageList[index];
        }
    }
}
