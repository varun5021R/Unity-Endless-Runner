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

    public TMP_Text coinsText;           
    public TMP_Text highCoinsText;       
    public TMP_Text gameOverCoinsText;   

    int score = 0;
    int coins = 0;

    int highScore;
    int highCoins;

    bool gameOver = false;

    void Awake()
    {
        Instance = this;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highCoins = PlayerPrefs.GetInt("HighCoins", 0);

        UpdateUI();
    }

    void Update()
    {
        if (!gameOver)
        {
            score += Mathf.RoundToInt(Time.deltaTime * 100f);
            scoreText.text = "Score : " + score;
        }

        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void AddCoin()
    {
        coins++;
        coinsText.text = "Coins: " + coins;
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        Time.timeScale = 0f;

        gameOverPanel.SetActive(true);

        if (mobileControls != null)
            mobileControls.SetActive(false);

        // Show collected coins
        gameOverCoinsText.text = "Coins Collected: " + coins;

        // -------- SCORE CHECK --------
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "NEW HIGH SCORE: " + highScore;
        }
        else
        {
            highScoreText.text = "High Score: " + highScore;
        }

        // -------- COINS CHECK --------
        if (coins > highCoins)
        {
            highCoins = coins;
            PlayerPrefs.SetInt("HighCoins", highCoins);
            highCoinsText.text = "NEW HIGH COINS: " + highCoins;
        }
        else
        {
            highCoinsText.text = "High Coins: " + highCoins;
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score : 0";
        coinsText.text = "Coins: 0";
        highScoreText.text = "High Score: " + highScore;
        highCoinsText.text = "High Coins: " + highCoins;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
