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

    public void Init()
    {
        score = 0;
        preScore = 0;
        scoreAmount = PlayerPrefs.GetFloat("speed");
    }

    void Update()
    {
        if (!GameManager.Instance.isPlay || GameManager.Instance.isMiniGame)
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
}
