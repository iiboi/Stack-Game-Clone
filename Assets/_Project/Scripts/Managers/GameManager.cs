using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton yapısı
    public static GameManager instance;
    
    [Header("References")]
    [SerializeField] CameraController cam;
    [SerializeField] BlockSpawnManager blockSpawnManager;

    // Oyunum 0 noktasından başladığı için, ve her bloğumun yüksekliği 0 olduğu için.
    // score değişkenini aynı zamanda yükseklik değişkeni için kullanacağım.
    // Örnek Score = 27; Yükseklik = 27;
    private int score = 0;
    private int highScore;
    private float decreaseTolerance = 0.02f;

    private void Awake() 
    {   
        instance = this;
    
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log($"HighScore {highScore}");
    }

    // Skoru Artır.
    public void IncreaseScore()
    {
        score++;
        Debug.Log($"Score: {score}");
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
            Debug.Log("Difficulty Level SET!");    
            if (blockSpawnManager.blockTolerance <= 0)
            {
                blockSpawnManager.blockTolerance = 0.01f;
            }
        }
    }
    public void GameOver()
    {
        cam.ShowTower(score);
        Debug.Log("GameOver!!!!");
    }
}
