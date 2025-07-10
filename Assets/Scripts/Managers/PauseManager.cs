using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject hudCanvas;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public Button startButton;
    public Button optionsButton;
    public Button quitButton;
    public PauseButton pauseButton;

    void Awake()
    {
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton").GetComponent<PauseButton>();
    }

    public void ResumeGame()
    {
        hudCanvas.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        pauseButton.isPaused = false;
    }

    public void PausedOptions()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void BackToStartMenu()
    {
        Time.timeScale = 1;
        pauseButton.isPaused = false;
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseButton.isPaused)
        {
            hudCanvas.SetActive(true);
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            pauseButton.isPaused = false;
        }
    }
}
