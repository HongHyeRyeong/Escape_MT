using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null) 
                instance = (UIManager)FindObjectOfType(typeof(UIManager));
            return instance;
        }
    }

    public Text score;
    public Image alcoholGauge;
    public GameObject gameResult;
    public Text[] gameResultScore;

    private float curGauge;
    private float saveGauge;

    public void Init()
    {
        score.text = "Score : 0";
        SetAlcoholGauge(0);
        gameResult.SetActive(false);
    }

    public void UpdateScore(float curScore)
    {
        score.text = "Score : " + curScore.ToString();
    }

    public void UpdateAlcoholGauge(float gauge)
    {
        curGauge = alcoholGauge.fillAmount * 100;
        saveGauge = gauge;

        if (saveGauge < curGauge)
            SetAlcoholGauge(saveGauge);
        else
            StartCoroutine(AnimAlcoholGauge());
    }

    private IEnumerator AnimAlcoholGauge()
    {
        while(curGauge < saveGauge)
        {
            curGauge += Time.deltaTime;
            SetAlcoholGauge(curGauge);

            yield return null;
        }
    }

    private void SetAlcoholGauge(float gauge)
    {
        alcoholGauge.fillAmount = gauge / 100f;

        if (80 < gauge)
            alcoholGauge.color = Color.red;
        else
            alcoholGauge.color = Color.white;
    }

    public void ShowGameResult(List<float> scores)
    {
        gameResult.SetActive(true);

        for (int i = 0; i < 3; ++i)
        {
            gameResultScore[i].text = "0";
        }

        for (int i = 0; i < 3 || i < scores.Count; ++i)
        {
            gameResultScore[i].text = scores[i].ToString();
        }
    }
}