using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject tilesMove;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] float spawnRate; 
    float nextSpawnTime = 0f;

    void Update()
    {
        if (GameManager.Instance.isStarted && !PlayerController.isGameOver)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnTile();
                nextSpawnTime = Time.time + 1f / spawnRate;
            }
        }
    }
    void SpawnTile()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-4, -9));
        GameObject tile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);

        tile.transform.SetParent(tilesMove.transform);
    }
}
