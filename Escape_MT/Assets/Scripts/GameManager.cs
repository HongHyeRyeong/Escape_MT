using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly float ItemSpawnTIme = 2f;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = (GameManager)FindObjectOfType(typeof(GameManager));
            return instance;
        }
    }

    public Transform items;
    public GameObject item;

    [HideInInspector]
    public bool isPlay;

    public void Init()
    {
        UIManager.Instance.Init();
        ScoreManager.Instance.Init();
        PlayerController.Instance.Init();
    }

    public void StartGame()
    {
        isPlay = true;

        InvokeRepeating("SpawnItem", ItemSpawnTIme, ItemSpawnTIme);
    }

    private void SpawnItem()
    {
        Debug.Log("SpawnItem");

        Vector3 pos = new Vector3(items.position.x, Random.Range(-1f, 1f), 0);
        GameObject newItem = Instantiate(item, pos, Quaternion.identity);
        newItem.transform.parent = items.transform;
    }

    public void GameOver()
    {
        isPlay = false;

        SoundManager.Instance.SetBGM("");
        SoundManager.Instance.SetEffect("Exit");

        SceneController.Instance.scores.Add(ScoreManager.Instance.score);
        SceneController.Instance.scores.Sort((a, b) => (a > b) ? -1 : 1);
        UIManager.Instance.ShowEnding(SceneController.Instance.scores);
    }
}
