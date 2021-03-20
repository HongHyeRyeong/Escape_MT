using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{

    public GameObject[] enemy;
    public GameObject[] spawnPos;

    int randEnemy;
    int randPos;

    public GameObject curEnemy;

    void Start()
    {

    }

    void Update()
    {

    }

    private void RandomSpawn()
    {
        randEnemy = Random.Range(0, 3);
        randPos = Random.Range(0, 2);
        curEnemy = Instantiate(enemy[randEnemy], spawnPos[randPos].transform);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SpawnPoint"))
        {
            RandomSpawn();
            Debug.Log("Spawn");
        }
    }


}
