using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToModelLoader : MonoBehaviour
{
    public string modelName;

    public void LoadModelScene()
    {
        PlayerPrefs.SetString("SelectedModelName", modelName);
        SceneManager.LoadScene("RedactorModel");
    }
}
