using UnityEngine;
using UnityEngine.UI;

public class ToggleScriptButton : MonoBehaviour
{
    public Button button; // Кнопка
    public MonoBehaviour scriptToToggle; // Скрипт, який потрібно вмикати/вимикати

    void Start()
    {
        // Перевірка наявності кнопки
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick); // Додаємо слухача події на натискання
        }
    }

    void OnButtonClick()
    {
        if (scriptToToggle != null)
        {
            // Перевірка, чи скрипт увімкнений, і зміна його стану
            scriptToToggle.enabled = !scriptToToggle.enabled;

            // Лог для перевірки стану скрипта
            if (scriptToToggle.enabled)
            {
                Debug.Log("Скрипт увімкнено");
            }
            else
            {
                Debug.Log("Скрипт вимкнено");
            }
        }
    }
}
