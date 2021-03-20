using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawn : MonoBehaviour
{

    public GameObject           Map;
    public Transform            spawnPoint;

    private float               cTime;
    public float                maxTime;

    void Start()
    {
        
    }

    void Update()
    {

        if(cTime >= maxTime)
        {
            InstantiateMap();
            cTime = 0f;
        }


        cTime += Time.deltaTime;
    }

    void InstantiateMap()
    {
        Instantiate(Map, spawnPoint.position, Quaternion.identity);
    }
}
