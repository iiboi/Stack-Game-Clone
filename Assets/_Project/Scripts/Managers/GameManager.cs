using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton yapısı
    public static GameManager instance;
    
    //Kamera Refaransı
    [SerializeField] CameraController cam;

    // Oyunum 0 noktasından başladığı için, ve her bloğumun yüksekliği 0 olduğu için.
    // score değişkenini aynı zamanda yükseklik değişkeni için kullanacağım.
    // Örnek Score = 27; Yükseklik = 27;
    public int score = 0;
    public int highScore;

    private void Awake() 
    {   
        instance = this;
    
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log($"HighScore {highScore}");
    }

    public void IncreaseScore()
    {
        score++;
        Debug.Log($"Score: {score}");

        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void GameOver()
    {
        cam.ShowTower(score);
        Debug.Log("GameOver!!!!");
    }
}
