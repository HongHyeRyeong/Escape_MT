using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Minigame1Controller : MonoBehaviour
{
    private readonly float GameTime = 40f;
    private float TurnTime = 7f;

    public Text script;
    public Text enemy;
    public Text player;
    public Slider totalTimeS;
    public Slider turnTimeS;

    void OnEnable()
    {
        TurnTime = 7f;

        enemy.text = "";
        player.text = "";
        script.gameObject.transform.parent.gameObject.SetActive(true);
        totalTimeS.gameObject.SetActive(false);
        totalTimeS.value = 1;
        turnTimeS.gameObject.SetActive(false);
        turnTimeS.value = 1;

        StartCoroutine(StartGame());
    }
    private IEnumerator StartGame()
    {
        script.text = "";
        yield return new WaitForSeconds(0.5f);

        script.text = "어른이 먼저지";
        yield return new WaitForSeconds(2f);
        script.gameObject.transform.parent.gameObject.SetActive(false);

        totalTimeS.gameObject.SetActive(true);
        turnTimeS.gameObject.SetActive(true);

        bool fail;

        bool turn = false;
        float totalTime = GameTime, turnTime = TurnTime;
        int index = 0;  // 0 탕 1 수 2 육
        while (true)
        {
            if (turn)
            {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Debug.Log("Input : 탕");

                    if ("탕수육".Substring(index, 1) == "탕수육".Substring(0, 1))
                    {
                        player.text = "탕수육".Substring(index, 1);

                        turn = false;
                        turnTime = TurnTime;

                        index++;
                        if (index == 3)
                            index = 0;
                    }
                    else
                    {
                        fail = true;
                        break;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Debug.Log("Input : 수");

                    if ("탕수육".Substring(index, 1) == "탕수육".Substring(1, 1))
                    {
                        player.text = "탕수육".Substring(index, 1);

                        turn = false;
                        turnTime = TurnTime;

                        index++;
                        if (index == 3)
                            index = 0;
                    }
                    else
                    {
                        fail = true;
                        break;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    if ("탕수육".Substring(index, 1) == "탕수육".Substring(2, 1))
                    {
                        player.text = "탕수육".Substring(index, 1);

                        turn = false;
                        turnTime = TurnTime;

                        index++;
                        if (index == 3)
                            index = 0;
                    }
                    else
                    {
                        fail = true;
                        break;
                    }
                }

                turnTime -= Time.deltaTime;
                if (turnTimeS.gameObject.activeSelf == false)
                    turnTimeS.gameObject.SetActive(true);
                turnTimeS.value = turnTime / TurnTime;
                if (turnTime < 0)
                {
                    fail = true;
                    break;
                }
            }
            else
            {
                enemy.text = "탕수육".Substring(index, 1);

                turn = true;
                TurnTime -= 0.3f;
                TurnTime = Mathf.Max(1f, TurnTime);

                index++;
                if (index == 3)
                    index = 0;
            }

            totalTime -= Time.deltaTime;
            totalTimeS.value = totalTime / GameTime;
            if (totalTime < 0)
            {
                fail = false;
                break;
            }

            yield return null;
        }

        gameObject.SetActive(false);
        GameManager.Instance.EndMiniGame(fail);
    }
}