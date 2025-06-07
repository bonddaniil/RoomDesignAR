using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public TMP_InputField searchInput;  
    public Transform buttonContainer;   

    private List<GameObject> allButtons = new List<GameObject>();

    void Start()
    {
        if (searchInput == null || buttonContainer == null)
        {
            Debug.LogError("SearchInput або ButtonContainer не призначені!");
            return;
        }

        foreach (Transform button in buttonContainer)
        {
            allButtons.Add(button.gameObject);
        }

        searchInput.onValueChanged.AddListener(delegate { FilterByName(); });

        FilterByName(); 
    }

    void FilterByName()
    {
        string searchText = searchInput.text.ToLower();
        Debug.Log("Пошук: " + searchText);

        foreach (GameObject button in allButtons)
        {
            RaitingParameters param = button.GetComponent<RaitingParameters>();
            if (param != null)
            {
                string objName = param.name.ToLower();
                bool match = objName.Contains(searchText);
                button.SetActive(match || string.IsNullOrEmpty(searchText));
            }
            else
            {
                button.SetActive(false); 
            }
        }
    }
}
