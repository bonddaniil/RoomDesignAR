using UnityEngine;
using UnityEngine.UI;

public class DropdownMenuController : MonoBehaviour
{
    private Button menuButton;
    private GameObject menuOptionsHolder;
    private GameObject background;

    private bool menuOpen = false;

    void Start()
    {
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
        menuOptionsHolder = GameObject.Find("MenuOptionsHolder");
        background = GameObject.Find("Background");

        if (menuButton == null || menuOptionsHolder == null || background == null)
        {
            Debug.LogError("Не знайдено один з об'єктів: MenuButton, MenuOptionsHolder або Background");
            return;
        }
        SetMenuVisible(false);

        menuButton.onClick.AddListener(ToggleMenu);

        Button[] childButtons = menuOptionsHolder.GetComponentsInChildren<Button>(true);
        foreach (Button btn in childButtons)
        {
            btn.onClick.AddListener(CloseMenu);
        }
    }

    void ToggleMenu()
    {
        menuOpen = !menuOpen;
        SetMenuVisible(menuOpen);
    }

    void CloseMenu()
    {
        menuOpen = false;
        SetMenuVisible(false);
    }

    void SetMenuVisible(bool visible)
    {
        menuOptionsHolder.SetActive(visible);
        background.SetActive(visible);

        foreach (Transform child in menuOptionsHolder.transform)
        {
            child.gameObject.SetActive(visible);
        }
    }
}