using UnityEngine;
using TMPro;

public class ModelActivator : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI modelNameText;

    void Start()
    {
        string selectedName = PlayerPrefs.GetString("SelectedModelName", "");

        foreach (Transform child in transform) 
        {
            var data = child.GetComponent<RedactorModelData>();
            if (data != null)
            {
                bool isActive = data.modelName == selectedName;
                child.gameObject.SetActive(isActive);

                if (isActive && modelNameText != null)
                {
                    modelNameText.text = "Model Name: " + data.modelName;
                }
            }
        }
    }
}
