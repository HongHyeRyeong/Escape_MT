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

    enum UpNDown
    {
        Up,
        Down
    }

    UpNDown upn;

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
        curEnemy = Instantiate(enemy[randEnemy], this.transform);

        Vector3 destination = spawnPos[randPos].transform.position;
        if(destination.y != curEnemy.transform.position.y)
        {
            if(upn == UpNDown.Up)
            {

            }
            else if(upn == UpNDown.Down)
            {

            }
        }
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
