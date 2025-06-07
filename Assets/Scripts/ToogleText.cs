using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextNavigator : MonoBehaviour
{
    
    [SerializeField] private string[] texts;

    private int[] nextIndex;
    private int[] prevIndex;
    private int currentIndex = 0;

    public TextMeshProUGUI inputField;
    public Button nextButton;
    public Button prevButton;

    void Start()
    {
        if (texts == null || texts.Length == 0)
        {
            Debug.LogError("TextNavigator: Масив текстів порожній!");
            return;
        }

        nextIndex = new int[texts.Length];
        prevIndex = new int[texts.Length];

        for (int i = 0; i < texts.Length; i++)
        {
            nextIndex[i] = (i + 1) % texts.Length;
            prevIndex[i] = (i - 1 + texts.Length) % texts.Length;
        }

        UpdateText();

        nextButton.onClick.AddListener(ShowNextText);
        prevButton.onClick.AddListener(ShowPreviousText);
    }

    private void UpdateText()
    {
        if (inputField != null && texts.Length > 0)
        {
            inputField.text = texts[currentIndex];
        }
    }

    private void ShowNextText()
    {
        currentIndex = nextIndex[currentIndex];
        UpdateText();
    }

    private void ShowPreviousText()
    {
        currentIndex = prevIndex[currentIndex];
        UpdateText();
    }
}
