using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
                instance = (ScoreManager)FindObjectOfType(typeof(ScoreManager));
            return instance;
        }
    }

    public float score;
    private List<float> scores;

    private float tempScore;
    private float scoreAmount;

    public void Init()
    {
        score = 0;
        scores = new List<float>();
        scoreAmount = PlayerPrefs.GetFloat("speed");
    }

    void Update()
    {
        tempScore += scoreAmount * Time.deltaTime;
        score = Mathf.Round(tempScore);
        UIManager.Instance.UpdateScore(score);
    }

    public void AddScore(float addScore)
    {
        score += addScore;
        UIManager.Instance.UpdateScore(score);
    }

    public void GameOver()
    {
        scores.Add(score);
        UIManager.Instance.ShowGameResult(scores);
    }
}
