using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public bool isPaused;
    public GameObject hudCanvas;
    public GameObject pauseMenu;

    void Awake()
    {
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            hudCanvas.SetActive(false);
        }
    }
}
