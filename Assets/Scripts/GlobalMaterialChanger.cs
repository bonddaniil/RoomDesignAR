using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GlobalMaterialChanger : MonoBehaviour
{
    [Header("UI та Стилі")]
    [SerializeField] private GameObject[] containers;
    [SerializeField] private TextMeshProUGUI styleNameText;

    [Header("Цільовий об'єкт (заповнюється автоматично)")]
    private Material[] materials;
    private string[] styleNames;
    private Sprite[] styleIcons;
    private Renderer targetRenderer;

    private Dictionary<Button, int> buttonMaterialPairs = new Dictionary<Button, int>();
    private Dictionary<int, GameObject> indexToOutline = new Dictionary<int, GameObject>();

    private string MaterialKey => $"material_{PlayerPrefs.GetString("SelectedModelName", "default")}";

    void Start()
    {
        LoadDataFromActiveModel();
        InitializeButtons();
        LoadSavedMaterial();
    }

    private void LoadDataFromActiveModel()
    {
        Transform modelHolder = GameObject.Find("ModelHolder")?.transform;
        if (modelHolder == null) return;

        foreach (Transform child in modelHolder)
        {
            if (child.gameObject.activeSelf)
            {
                RedactorModelData data = child.GetComponent<RedactorModelData>();
                if (data != null)
                {
                    materials = data.materials;
                    styleNames = data.styleNames;
                    styleIcons = data.styleIcons;

                    if (data.modelPrefab != null)
                    {
                        targetRenderer = data.modelPrefab.GetComponent<Renderer>();
                    }
                }
            }
        }
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < containers.Length; i++)
        {
            int materialIndex = i;
            Button btn = containers[i].GetComponentInChildren<Button>();
            Transform outline = containers[i].transform.Find("Outline");

         
            Image img = btn?.GetComponent<Image>();
            if (img != null && styleIcons != null && materialIndex < styleIcons.Length)
            {
                img.sprite = styleIcons[materialIndex];
            }

            if (btn != null)
            {
                buttonMaterialPairs.Add(btn, materialIndex);
                btn.onClick.AddListener(() => OnMaterialButtonClicked(materialIndex));

                if (outline != null)
                {
                    indexToOutline[materialIndex] = outline.gameObject;
                    outline.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnMaterialButtonClicked(int materialIndex)
    {
        if (materialIndex < 0 || materialIndex >= materials.Length) return;

        ApplyMaterialByIndex(materialIndex);
        UpdateOutlines(materialIndex);
        UpdateStyleName(materialIndex);
    }

    public void ApplyMaterialByIndex(int index)
    {
        if (materials == null || targetRenderer == null) return;

        if (index >= 0 && index < materials.Length)
        {
            targetRenderer.sharedMaterial = materials[index];
            PlayerPrefs.SetInt(MaterialKey, index);
            PlayerPrefs.DeleteKey(MaterialKey + "_name");
            PlayerPrefs.Save();
        }
    }

    private void LoadSavedMaterial()
    {
        string customName = PlayerPrefs.GetString(MaterialKey + "_name", "");

        if (!string.IsNullOrEmpty(customName))
        {
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] != null && materials[i].name == customName)
                {
                    targetRenderer.sharedMaterial = materials[i];
                    UpdateOutlines(i);
                    UpdateStyleName(i);
                    return;
                }
            }
        }

        int savedIndex = PlayerPrefs.GetInt(MaterialKey, -1);
        if (savedIndex >= 0 && savedIndex < materials.Length)
        {
            targetRenderer.sharedMaterial = materials[savedIndex];
            UpdateOutlines(savedIndex);
            UpdateStyleName(savedIndex);
        }
    }

    private void UpdateOutlines(int activeIndex)
    {
        foreach (var kvp in indexToOutline)
        {
            kvp.Value.SetActive(kvp.Key == activeIndex);
        }
    }

    private void UpdateStyleName(int index)
    {
        if (styleNameText != null && styleNames != null && index < styleNames.Length)
        {
            styleNameText.text = "Current Style: " + styleNames[index];
        }
    }
}
