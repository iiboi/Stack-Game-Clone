using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton yapısı
    public static GameManager instance;

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
        Debug.Log("GameOver!!!!");
    }
}
