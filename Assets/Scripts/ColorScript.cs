using UnityEngine;
using UnityEngine.UI; 

public class SaveChairColor : MonoBehaviour
{
    public Button saveButton;

    private void Start()
    {
        
        
        saveButton.onClick.AddListener(SaveData);
        
     
    }

    private void SaveData()
    {
        
        PlayerPrefs.SetString("Chair", "Green");
        PlayerPrefs.Save(); 
       
    }
}
