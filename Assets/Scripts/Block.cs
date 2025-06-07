using UnityEngine;
using UnityEngine.UI;

public class ToggleScriptButton : MonoBehaviour
{
    public Button button; // ������
    public MonoBehaviour scriptToToggle; // ������, ���� ������� �������/��������

    void Start()
    {
        // �������� �������� ������
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick); // ������ ������� ��䳿 �� ����������
        }
    }

    void OnButtonClick()
    {
        if (scriptToToggle != null)
        {
            // ��������, �� ������ ���������, � ���� ���� �����
            scriptToToggle.enabled = !scriptToToggle.enabled;

            // ��� ��� �������� ����� �������
            if (scriptToToggle.enabled)
            {
                Debug.Log("������ ��������");
            }
            else
            {
                Debug.Log("������ ��������");
            }
        }
    }
}
