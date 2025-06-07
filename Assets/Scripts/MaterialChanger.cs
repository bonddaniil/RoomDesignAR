using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material[] materials;
    public Renderer targetRenderer;

    private string MaterialKey => $"material_{PlayerPrefs.GetString("SelectedModelName", "default")}";

    void Start()
    {
        if (targetRenderer != null)
        {
            LoadSavedMaterial();
        }
        else
        {
            Debug.LogWarning("MaterialChanger: targetRenderer не призначений!");
        }
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
        if (targetRenderer == null) return;

        string customName = PlayerPrefs.GetString(MaterialKey + "_name", "");

        if (!string.IsNullOrEmpty(customName))
        {
            foreach (var mat in materials)
            {
                if (mat != null && mat.name == customName)
                {
                    targetRenderer.sharedMaterial = mat;
                    return;
                }
            }
        }

        int savedIndex = PlayerPrefs.GetInt(MaterialKey, -1);
        if (savedIndex >= 0 && savedIndex < materials.Length)
        {
            targetRenderer.sharedMaterial = materials[savedIndex];
        }
    }
}
