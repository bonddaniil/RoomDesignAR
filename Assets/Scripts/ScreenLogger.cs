using UnityEngine;
using TMPro;

public class ScreenLogger : MonoBehaviour
{
    public TextMeshProUGUI logText;
    [TextArea] public string logContent = "";
    public int maxLines = 15;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string prefix = "";
        switch (type)
        {
            case LogType.Warning: prefix = "<color=yellow>[W]</color> "; break;
            case LogType.Error:
            case LogType.Exception: prefix = "<color=red>[E]</color> "; break;
            default: prefix = "<color=white>"; break;
        }

        logContent += prefix + logString + "</color>\n";

     
        var lines = logContent.Split('\n');
        if (lines.Length > maxLines)
        {
            logContent = string.Join("\n", lines, lines.Length - maxLines - 1, maxLines);
        }

        if (logText != null)
        {
            logText.text = logContent;
        }
    }
}
