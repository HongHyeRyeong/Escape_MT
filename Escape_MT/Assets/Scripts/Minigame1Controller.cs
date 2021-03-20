using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame1Controller : MonoBehaviour
{
    private bool gameRun;

    private int sequence;
    private bool myTurn;

    [SerializeField] private float maxTime;
    [SerializeField] private float curTime;

    [SerializeField] private float maxGameTime;
    [SerializeField] private float curGameTime;

    [SerializeField] private Slider slider;
    [SerializeField] private Text enemyTxt;
    [SerializeField] private Text playerTxt;

    void OnEnable()
    {
        sequence = 1;
        myTurn = false;
        curGameTime = 0;
        curTime = maxTime;
        gameRun = true;
    }

    void Update()
    {
        if(curGameTime <= maxGameTime && gameRun)
        {
            MiniGameSystem();
            EnemySystem();
        }
        else
        {
            this.gameObject.SetActive(false);
            GameManager.Instance.EndMiniGame(true);
        }
        slider.maxValue = maxTime;
        slider.minValue = 0;
        slider.value = curTime;

        curGameTime += Time.deltaTime;
    }

    private void MiniGameSystem()
    {
        if (myTurn)
        {
            if (curTime >= 0)
            {
                Debug.Log("내 차례 : " + sequence);
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    SequenceChk(1);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    SequenceChk(2);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    if (sequence == 3)
                    {
                        PrintSequence();
                        sequence = 1;
                    }
                    else
                    {
                        GameOver();
                    }

                    myTurn = false;
                }

            }
            else
            {
                GameOver();
            }
        }
        curTime -= Time.deltaTime;

    }

    private void SequenceChk(int _sequence)
    {
        if(sequence == _sequence)
        {
            PrintSequence();
            sequence++;
        }
        else
        {
            GameOver();
        }

        myTurn = false;
    }

    private void EnemySystem()
    {
        if (!myTurn)
        {
            curTime = maxTime;
            Debug.Log("선배 차례 : " + sequence);
            PrintSequence();
            myTurn = true;
            sequence++;
            if (sequence == 4)
            {
                sequence = 1;
            }
        }
    }

    private void PrintSequence()
    {
        switch (sequence)
        {
            case 1:
                Debug.Log("탕");
                if (!myTurn)
                {
                    enemyTxt.text = "탕";
                }
                else
                {
                    playerTxt.text = "탕";
                }
                break;
            case 2:
                Debug.Log("수");
                if (!myTurn)
                {
                    enemyTxt.text = "수";
                }
                else
                {
                    playerTxt.text = "수";
                }
                break;
            case 3:
                Debug.Log("육");
                if (!myTurn)
                {
                    enemyTxt.text = "육";
                }
                else
                {
                    playerTxt.text = "육";
                }
                break;
            default:
                Debug.Log("아웃");
                break;
        }

        if (sequence == 4)
        {
            sequence = 1;
        }
    }

    private void GameOver()
    {
        gameRun = false;
        gameObject.SetActive(false);

        GameManager.Instance.EndMiniGame(false);
    }
}
