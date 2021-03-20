using UnityEngine;
using UnityEngine.SceneManagement;
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
        SoundManager.Instance.SetBGM("InGame");
        SceneManager.LoadScene("GameScene");
    }

    public void ToTitleScene()
    {
        SoundManager.Instance.SetBGM("Title");
        SceneManager.LoadScene("TitleScene");
    }

    public void ToMinigameScene()
    {

    }
}
