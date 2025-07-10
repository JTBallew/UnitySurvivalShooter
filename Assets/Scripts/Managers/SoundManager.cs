using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public static SoundManager Instance;
    public float musicVolume;
    public float sfxVolume;
    public Button musicButton;
    public Button sfxButton;
    public Button backbutton;
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI sfxText;
    public GameObject pauser;
    public PauseButton pauseButton;

    void Awake()
    {
        Instance = this;
        pauseButton = pauser.GetComponent<PauseButton>();
    }

    void Start()
    {
        LoadVolumes();
        optionsMenu.SetActive(false);
    }

    public void MusicChange()
    {
        if (musicVolume == 1f)
        {
            musicVolume = 0.5f;
            musicText.text = "Music - Low";
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetString("MusicText", musicText.text);
        }
        else if (musicVolume == 0.5f)
        {
            musicVolume = 0f;
            musicText.text = "Music - Off";
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetString("MusicText", musicText.text);
        }
        else if (musicVolume == 0f)
        {
            musicVolume = 1f;
            musicText.text = "Music - High";
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            PlayerPrefs.SetString("MusicText", musicText.text);
        }
        SoundMixer.Instance.MusicChange();
    }

    public void SfxChange()
    {
        if (sfxVolume == 1f)
        {
            sfxVolume = 0.5f;
            sfxText.text = "SFX - Low";
            PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
            PlayerPrefs.SetString("SfxText", sfxText.text);
        }
        else if (sfxVolume == 0.5f)
        {
            sfxVolume = 0f;
            sfxText.text = "SFX - Off";
            PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
            PlayerPrefs.SetString("SfxText", sfxText.text);
        }
        else if (sfxVolume == 0f)
        {
            sfxVolume = 1f;
            sfxText.text = "SFX - High";
            PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
            PlayerPrefs.SetString("SfxText", sfxText.text);
        }
        SoundMixer.Instance.sfxChange();
    }

    public void BackToMenu()
    {
        if (pauseButton.isPaused)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            startMenu.SetActive(true);
        }
        optionsMenu.SetActive(false);
    }

    public void LoadVolumes()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicText.text = PlayerPrefs.GetString("MusicText");
        }
        else
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
            PlayerPrefs.SetString("MusicText", "Music - High");
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicText.text = PlayerPrefs.GetString("MusicText");
        }

        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
            sfxText.text = PlayerPrefs.GetString("SfxText");
        }
        else
        {
            PlayerPrefs.SetFloat("SfxVolume", 1f);
            PlayerPrefs.SetString("SfxText", "SFX - High");
            sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
            sfxText.text = PlayerPrefs.GetString("SfxText");
        }
        SoundMixer.Instance.MusicChange();
        SoundMixer.Instance.sfxChange();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseButton.isPaused)
            {
                pauseMenu.SetActive(true);
            }
            else
            {
                startMenu.SetActive(true);
            }
            optionsMenu.SetActive(false);
        }
    }
}
