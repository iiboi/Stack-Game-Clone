using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;

    private void Awake() 
    {
        instance = this;
    }

    public void IncreaseScore()
    {
        score++;

        Debug.Log($"Score: {score}");
    }

    public void GameOver()
    {
        Debug.Log("GameOver!!!!");
    }
}
