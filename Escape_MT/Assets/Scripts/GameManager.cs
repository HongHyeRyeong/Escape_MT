using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly float ItemSpawnTIme = 4f;

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

    [SerializeField]
    private GameObject miniGame1;
    [SerializeField]
    private GameObject miniGame2;

    [HideInInspector]
    public bool isPlay;
    [HideInInspector]
    public bool isMiniGame;

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

    public void GameOver()
    {
        isPlay = false;

        SoundManager.Instance.SetBGM("");
        SoundManager.Instance.SetEffect("Exit");

        SceneController.Instance.scores.Add(ScoreManager.Instance.score);
        SceneController.Instance.scores.Sort((a, b) => (a > b) ? -1 : 1);
        UIManager.Instance.ShowEnding(SceneController.Instance.scores);
    }

    private void SpawnItem()
    {
        Debug.Log("SpawnItem");

        Vector3 pos = new Vector3(items.position.x, Random.Range(-1f, 1f), 0);
        GameObject newItem = Instantiate(item, pos, Quaternion.identity);
        newItem.transform.parent = items.transform;
    }

    public void StartMinigame(int type)
    {
        isMiniGame = true;

        StartCoroutine(SceneController.Instance.ShowFade((x) =>
        {
            if (type == 1)
                miniGame1.SetActive(true);
            else if (type == 2)
                miniGame2.SetActive(true);
        }));
    }

    public void EndMiniGame(bool fail)
    {
        StartCoroutine(SceneController.Instance.ShowFade((x) =>
        {
            Invoke("EndMiniGameAndStartGame", 1);
            StartCoroutine(UIManager.Instance.ShowMiniGameEnding(fail));
            if (fail)
                PlayerController.Instance.AttackSenior();
        }));
    }

    private void EndMiniGameAndStartGame()
    {
        isMiniGame = false;
    }
}
