using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void ToIngameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ToMinigameScene()
    {

    }
}
