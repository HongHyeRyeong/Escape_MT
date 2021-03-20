using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Minigame2Controller : MonoBehaviour
{
    private readonly int GameCount = 6;

    public Text script;

    private int[] dirs = new int[6];

    private void OnEnable()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        script.text = "...";
        yield return new WaitForSeconds(1f);

        script.text = "";
        yield return new WaitForSeconds(0.5f);

        script.text = "춤 좀 추냐? 따라해봐라";
        yield return new WaitForSeconds(2f);

        bool fail = false;
        int index = 0, count = 1;
        while (count <= GameCount)
        {
            Debug.Log("Turn : " + count);

            dirs[index] = Random.Range(1, 5);

            if (dirs[index] == 1)
                script.text = "위";
            else if (dirs[index] == 2)
                script.text = "아래";
            else if (dirs[index] == 3)
                script.text = "왼";
            else if (dirs[index] == 4)
                script.text = "오";

            yield return new WaitForSeconds(1f);
            script.text = "해봐라";

            index++;
            count++;

            fail = false;
            int index2 = 0;
            int[] input = new int[index];
            while(index2 < index)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Debug.Log("Input : 위");

                    input[index2] = 1;

                    if(input[index2] != dirs[index2])
                    {
                        fail = true;
                        break;
                    }

                    index2++;
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Debug.Log("Input : 아래");

                    input[index2] = 2;

                    if (input[index2] != dirs[index2])
                    {
                        fail = true;
                        break;
                    }

                    index2++;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Debug.Log("Input : 왼");

                    input[index2] = 3;

                    if (input[index2] != dirs[index2])
                    {
                        fail = true;
                        break;
                    }

                    index2++;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Debug.Log("Input : 오");

                    input[index2] = 4;

                    if (input[index2] != dirs[index2])
                    {
                        fail = true;
                        break;
                    }

                    index2++;
                }

                yield return null;
            }

            if (fail)
                break;

            script.text = "제법이군";
            yield return new WaitForSeconds(1f);
        }

        gameObject.SetActive(false);
        GameManager.Instance.EndMiniGame(fail);
    }
}
