using UnityEngine;

public class AutoScaler : MonoBehaviour
{
    [Header("Налаштування масштабування")]
    public float targetHeight = 1.0f; 

    void Start()
    {
        NormalizeHeight();
    }

    void NormalizeHeight()
    {
        Bounds bounds = GetRenderableBounds(gameObject);
        float currentHeight = bounds.size.y;

        if (currentHeight > 0.001f)
        {
            float scaleMultiplier = targetHeight / currentHeight;
            transform.localScale *= scaleMultiplier;

            Debug.Log($"[AutoScaler] Масштабовано '{gameObject.name}' до висоти {targetHeight} м. Множник масштабу: {scaleMultiplier}");
        }
        else
        {
            Debug.LogWarning($"[AutoScaler] '{gameObject.name}' має нульову висоту! Перевір MeshRenderer.");
        }
    }

    Bounds GetRenderableBounds(GameObject go)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0)
            return new Bounds(go.transform.position, Vector3.zero);

        Bounds combinedBounds = renderers[0].bounds;
        foreach (Renderer r in renderers)
        {
            combinedBounds.Encapsulate(r.bounds);
        }

        return combinedBounds;
    }
}
