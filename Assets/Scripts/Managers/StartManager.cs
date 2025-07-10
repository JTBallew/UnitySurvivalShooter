using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject optionsMenu;
    public Button startButton;
    public Button optionsButton;
    public Button quitButton;

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchToOptions()
    {
        optionsMenu.SetActive(true);
        startMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
