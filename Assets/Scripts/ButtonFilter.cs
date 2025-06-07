using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonFilter : MonoBehaviour
{
    public TMP_Dropdown categoryDropdown;
    public Transform buttonContainer;

    private Dictionary<string, List<GameObject>> categoryButtons = new Dictionary<string, List<GameObject>>();

    void Start()
    {
        if (categoryDropdown == null || buttonContainer == null)
            return;

        categoryButtons["all"] = new List<GameObject>();
        categoryButtons["chairs"] = new List<GameObject>();
        categoryButtons["sofas"] = new List<GameObject>();
        categoryButtons["tables"] = new List<GameObject>();
        categoryButtons["other"] = new List<GameObject>();

        foreach (Transform button in buttonContainer)
        {
            RaitingParameters category = button.GetComponent<RaitingParameters>();
            if (category != null)
            {
                string categoryName = category.category.ToLower();

                if (!categoryButtons.ContainsKey(categoryName))
                    categoryButtons[categoryName] = new List<GameObject>();

                categoryButtons[categoryName].Add(button.gameObject);
                categoryButtons["all"].Add(button.gameObject);
            }
        }

        categoryDropdown.onValueChanged.AddListener(delegate { FilterButtons(); });

        FilterButtons(); 
    }

    void FilterButtons()
    {
        string selected = categoryDropdown.options[categoryDropdown.value].text.Trim().ToLower();

        if (selected == "all")
        {
            foreach (var button in categoryButtons["all"])
                button.SetActive(true);
            return;
        }

        if (selected == "comfort" || selected == "ergonomics" || selected == "universality")
        {
            List<(GameObject button, int rating)> sorted = new List<(GameObject, int)>();

            foreach (GameObject button in categoryButtons["all"])
            {
                RaitingParameters rp = button.GetComponent<RaitingParameters>();
                if (rp != null)
                {
                    int value = 0;
                    switch (selected)
                    {
                        case "comfort": value = rp.comfort; break;
                        case "ergonomics": value = rp.ergonomics; break;
                        case "universality": value = rp.universality; break;
                    }

                    sorted.Add((button, value));
                    button.SetActive(true); 
                }
            }

            sorted.Sort((a, b) => b.rating.CompareTo(a.rating));

            for (int i = 0; i < sorted.Count; i++)
                sorted[i].button.transform.SetSiblingIndex(i);

            return;
        }

        foreach (var category in categoryButtons)
        {
            foreach (var button in category.Value)
                button.SetActive(false);
        }

        if (categoryButtons.ContainsKey(selected))
        {
            foreach (var button in categoryButtons[selected])
                button.SetActive(true);
        }
    }
}
