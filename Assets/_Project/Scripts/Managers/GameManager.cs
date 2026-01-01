using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton yapısı
    public static GameManager instance;
    
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] BlockSpawnManager blockSpawnManager;

    private int score = 0;
    private int highScore;
    private float decreaseTolerance = 0.01f;

    private void Awake() 
    {   
        instance = this;
        
        Application.targetFrameRate = 60;
        
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Skoru Artır.
    public void IncreaseScore()
    {
        score++;
        
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateScore(score);
        }

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
        }
        
        DecreaseTolerance();
    }

    // Zorluk seviyesini ayarla.
    private void DecreaseTolerance()
    {
        if (score % 10 == 0)
        {
            blockSpawnManager.blockTolerance -= decreaseTolerance;
             
            if (blockSpawnManager.blockTolerance <= 0)
            {
                blockSpawnManager.blockTolerance = 0.01f;
            }
        }
    }

    public void GameOver()
    {
        cameraController.ShowTower(score);
        // Debug.Log("GameOver!!!!");

        if (UIManager.instance != null)
        {
            UIManager.instance.ShowGameOverScreen(score);
        }
    }
}