using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private readonly float ItemSpawnTIme = 1f;

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

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        isPlay = true;

        UIManager.Instance.Init();
        ScoreManager.Instance.Init();
        PlayerController.Instance.Init();

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

        ScoreManager.Instance.GameOver();
    }

    public void GameRestart()
    {
        Init();
    }
}
