using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public GameObject gameOverPanel;
    public GameObject mobileControls;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private int score = 0;
    private int highScore = 0;

    private bool gameOver = false;

    void Awake()
    {
        Instance = this;

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateUI();
    }

    void Update()
    {
        if (!gameOver)
        {
            score += Mathf.RoundToInt(Time.deltaTime * 100f);

            if (scoreText != null)
                scoreText.text = "Score : " + score;
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (mobileControls != null)
            mobileControls.SetActive(false);

        // Let CoinManager handle coins UI
        if (CoinManager.instance != null)
            CoinManager.instance.GameOver();

        // -------- SCORE CHECK --------
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            if (highScoreText != null)
                highScoreText.text = "NEW HIGH SCORE: " + highScore;
        }
        else
        {
            if (highScoreText != null)
                highScoreText.text = "High Score: " + highScore;
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score : 0";

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
