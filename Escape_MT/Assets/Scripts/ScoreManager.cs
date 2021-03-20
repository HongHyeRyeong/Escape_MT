using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private readonly int Decrease_AlcoholGauge = 50;

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
    private float preScore;
    private float scoreAmount;
    private List<float> scores;

    public void Init()
    {
        score = 0;
        preScore = 0;
        scores = new List<float>();
        scoreAmount = PlayerPrefs.GetFloat("speed");
    }

    void Update()
    {
        if (!GameManager.Instance.isPlay)
            return;

        score += scoreAmount * Time.deltaTime;
        UIManager.Instance.UpdateScore(score);

        if(preScore + Decrease_AlcoholGauge <= score)
        {
            preScore = score;
            PlayerController.Instance.DecreaseAlcoholGauge();
        }
    }

    public void AddScore(float addScore)
    {
        score += addScore;
        UIManager.Instance.UpdateScore(score);
    }

    public void GameOver()
    {
        scores.Add(score);
        UIManager.Instance.ShowEnding(scores);
    }
}
