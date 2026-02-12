using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("UI")]
    public TextMeshProUGUI coinTextTopRight;       // Coins at top right during game
    public TextMeshProUGUI gameOverCoinText;       // Coins collected in game over
    public TextMeshProUGUI highCoinText;           // High coins in game over

    private int currentCoins = 0;
    private int highCoins = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        highCoins = PlayerPrefs.GetInt("HighCoins", 0);
        UpdateUI();
    }

    public void AddCoin()
    {
        currentCoins++;
        UpdateUI();
    }

    public int GetCurrentCoins()
    {
        return currentCoins;
    }

    public void ResetCoins()
    {
        currentCoins = 0;
        UpdateUI();
    }
    

public void UpdateCoins(int currentCoins)
{
    if (currentCoins > highCoins)
    {
        highCoins = currentCoins;
        PlayerPrefs.SetInt("HighCoins", highCoins);
        PlayerPrefs.Save();
    }
}


    public void GameOver()
    {
        bool newHigh = false;

        if (currentCoins > highCoins)
        {
            highCoins = currentCoins;
            PlayerPrefs.SetInt("HighCoins", highCoins);
            newHigh = true;
        }
        if (coinTextTopRight != null)
    coinTextTopRight.gameObject.SetActive(false);


        // Game Over UI
        if (gameOverCoinText != null)
            gameOverCoinText.text = "Coins Collected: " + currentCoins;

        if (highCoinText != null)
        {
            if (newHigh)
                highCoinText.text = "NEW HIGH COINS: " + highCoins;
            else
                highCoinText.text = "High Coins: " + highCoins;
        }
    }

    void UpdateUI()
    {
        if (coinTextTopRight != null)
            coinTextTopRight.text = "Coins: " + currentCoins;
    }
}
