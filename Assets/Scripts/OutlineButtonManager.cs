using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OutlineButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject[] containers;

    private Dictionary<Button, GameObject> buttonOutlinePairs = new Dictionary<Button, GameObject>();

    void Start()
    {
        
        foreach (GameObject container in containers)
        {
            Button btn = container.GetComponentInChildren<Button>();
            Transform outline = container.transform.Find("Outline");

            if (btn != null && outline != null)
            {
                buttonOutlinePairs.Add(btn, outline.gameObject);

                outline.gameObject.SetActive(false);

                btn.onClick.AddListener(() => OnButtonClicked(btn));
            }
            else
            {
                Debug.LogWarning($"Button або Outline не знайдені у контейнері {container.name}");
            }
        }
    }

    private void OnButtonClicked(Button clickedButton)
    {
        foreach (var pair in buttonOutlinePairs)
        {
         
            pair.Value.SetActive(pair.Key == clickedButton);
        }
    }
}
