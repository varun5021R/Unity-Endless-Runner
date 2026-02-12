using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private float score;
    private int highScore;
    private bool gameOver = false;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update()
    {
        if (gameOver) return;

        score += Time.deltaTime * 40f;   // 🔥 MULTIPLIER 40

        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    public void GameOver()
    {
        gameOver = true;

        int finalScore = Mathf.FloorToInt(score);

        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "NEW HIGH SCORE: " + highScore;
        }
        else
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }

    public void ResetScore()
    {
        score = 0;
        gameOver = false;
    }
}
