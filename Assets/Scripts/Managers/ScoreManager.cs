using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score;
    public int highScore;
    public TextMeshProUGUI text;
    public TextMeshProUGUI textHighScore;

    void Awake()
    {
        Instance = this;
        score = 0;
        LoadGameState();
        textHighScore.text = "High Score: " + highScore;
    }

    public void ShowScore()
    {
        text.text = "Score: " + score;
        if (score > highScore)
        {
            highScore = score;
            textHighScore.text = "High Score: " + highScore;
            SaveGameState();
        }
    }

    void SaveGameState()
    {
        //Save Variable
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    void LoadGameState()
    {
        //Load Variable
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
