using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject restartScreen;
    [SerializeField] BlockSpawnManager blockSpawnManager;

    [Header("Score Texts")]
    [SerializeField] TextMeshProUGUI gameScoreText;
    [SerializeField] TextMeshProUGUI startBestScoreText;
    [SerializeField] TextMeshProUGUI restartCurrentScoreText;

    public static UIManager instance;

    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        startScreen.SetActive(true);
        gameScreen.SetActive(false);
        restartScreen.SetActive(false);

        startBestScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void GameStart()
    {
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
        restartScreen.SetActive(false);
        
        blockSpawnManager.ActivateGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateScore(int currentScore)
    {
        gameScoreText.text = currentScore.ToString();
    }

    // İSMİNİ DEĞİŞTİRDİM: Hem skoru yazıyor hem de ekranı açıyor.
    public void ShowGameOverScreen(int score)
    {
        gameScreen.SetActive(false);
        restartScreen.SetActive(true); // Restart ekranını aç!

        if (restartCurrentScoreText != null)
        {
            restartCurrentScoreText.text = score.ToString();
        }
    }
}