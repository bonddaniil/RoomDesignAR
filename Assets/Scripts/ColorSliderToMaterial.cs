using UnityEngine;
using UnityEngine.UI;

public class ColorSliderToMaterial : MonoBehaviour
{
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Button applyButton;

    private Material customMaterial;

    void Start()
    {
        LoadCustomMaterialFromActiveModel();
        applyButton.onClick.AddListener(ApplyColorFromSliders);
    }

    private void LoadCustomMaterialFromActiveModel()
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
                    customMaterial = data.custom;
                    break;
                }
            }
        }
    }

    private void ApplyColorFromSliders()
    {
        if (customMaterial == null)
            return;

        Color newColor = new Color(
            redSlider.value,
            greenSlider.value,
            blueSlider.value
        );

        customMaterial.color = newColor;

        PlayerPrefs.SetFloat("color_r", redSlider.value);
        PlayerPrefs.SetFloat("color_g", greenSlider.value);
        PlayerPrefs.SetFloat("color_b", blueSlider.value);
        PlayerPrefs.Save();
    }
}


