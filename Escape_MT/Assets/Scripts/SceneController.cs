using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
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

    public void ToGameoverScene()
    {

    }
}
