using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject fade;

    public void OnPlayButtonPressed()
    {
        LoadingManager.Manager.StartLoading("ForestDay");
    }

    public void OnOptionsButtonPressed()
    {
        if (optionsMenu == null) return;

        if (mainMenu.activeSelf && !optionsMenu.activeSelf)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
    }

    public void OnQuitButtonPressed()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            LoadingManager.Manager.StartLoading("MainMenu");
            Time.timeScale = 1f;
        }
        else
        {
            Application.Quit();
        }
    }

    public void OnCloseButtonPressed()
    {
        if (!mainMenu.activeSelf && optionsMenu.activeSelf)
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }
}