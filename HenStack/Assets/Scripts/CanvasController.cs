using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;
    public GameObject gameScreen;
    public GameObject gameOverScreen;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        SetScoreText(0);
        gameScreen.SetActive(true);
        gameOverScreen.SetActive(false);

    }

    public void SetScoreText(int score)
    {
        scoreText.text = "SCORE: " + score;
    }

    public void GameOver(int score)
    {
        gameOverScreen.SetActive(true);
        gameScreen.SetActive(false);
        gameOverScoreText.text = "SCORE: " + score;
    }

    public void Retry()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Debug.Log("MenuScreen!");
    }
}
