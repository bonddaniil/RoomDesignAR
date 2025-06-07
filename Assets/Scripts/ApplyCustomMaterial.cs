using UnityEngine;
using UnityEngine.UI;

public class ApplyCustomMaterial : MonoBehaviour
{
    [Header("UI")]
    public Button applyMaterialButton;

    private Renderer targetRenderer;
    private Material customMaterial;
    private string MaterialKey => $"material_{PlayerPrefs.GetString("SelectedModelName", "default")}";

    void Start()
    {
        LoadCustomMaterialFromModelData();
        applyMaterialButton.onClick.AddListener(ApplyMaterial);
        LoadMaterial();
    }

    private void LoadCustomMaterialFromModelData()
    {
        Transform modelHolder = GameObject.Find("ModelHolder")?.transform;
        if (modelHolder == null) return;

        foreach (Transform child in modelHolder)
        {
            if (child.gameObject.activeSelf)
            {
                RedactorModelData data = child.GetComponent<RedactorModelData>();
                if (data != null && data.modelPrefab != null)
                {
                    targetRenderer = data.modelPrefab.GetComponent<Renderer>();
                    customMaterial = data.custom;
                    break;
                }
            }
        }

        if (targetRenderer == null || customMaterial == null)
        {
            Debug.LogWarning("ApplyCustomMaterial: Renderer or custom material not found.");
        }
    }

    private void ApplyMaterial()
    {
        if (targetRenderer == null || customMaterial == null) return;

        targetRenderer.sharedMaterial = customMaterial;

        PlayerPrefs.SetString(MaterialKey + "_name", customMaterial.name);
        PlayerPrefs.DeleteKey(MaterialKey);
        PlayerPrefs.Save();
    }

    private void LoadMaterial()
    {
        if (targetRenderer == null || customMaterial == null) return;

        string savedName = PlayerPrefs.GetString(MaterialKey + "_name", "");
        if (savedName == customMaterial.name)
        {
            targetRenderer.sharedMaterial = customMaterial;
        }
    }
}