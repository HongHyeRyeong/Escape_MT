using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneController : MonoBehaviour
{
    private static SceneController instance;
    public static SceneController Instance
    {
        get
        {
            if (instance == null)
                instance = (SceneController)FindObjectOfType(typeof(SceneController));
            return instance;
        }
    }

    public List<float> scores = new List<float>();
    public Image fade;

    private void Awake()
    {
        if(instance !=null)
        {
            Destroy(gameObject);
            return;
        }

        Screen.SetResolution(854, 480, false);
        DontDestroyOnLoad(this);

    }

    public void ToIngameScene()
    {
        StartCoroutine(ShowFade((x) =>
        {
            SceneManager.LoadScene("GameScene");
            SoundManager.Instance.SetBGM("InGame");
        }));
    }

    public void ToTitleScene()
    {
        StartCoroutine(ShowFade((x) =>
        {
            SceneManager.LoadScene("TitleScene");
            SoundManager.Instance.SetBGM("Title");
        }));
    }

    public IEnumerator ShowFade(System.Action<bool> callback)
    {
        fade.gameObject.SetActive(true);

        Color color = fade.color;

        // Fade In
        float time = 0;
        color.a = 0;
        while (color.a < 1)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, time);
            fade.color = color;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        callback(true);

        // Fade Out
        time = 0;
        color.a = 1;
        while (color.a > 0)
        {
            time += Time.deltaTime * 3;
            color.a = Mathf.Lerp(1, 0, time);
            fade.color = color;

            yield return null;
        }

        fade.gameObject.SetActive(false);
    }
}
