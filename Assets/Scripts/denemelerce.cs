using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class denemelerce : MonoBehaviour
{
    [SerializeField] GameObject tilesMove;
    [SerializeField] AssetReference tilePrefabReference;
    [SerializeField] float spawnRate;
    float nextSpawnTime = 0f;

    Queue<GameObject> tilePool = new Queue<GameObject>();

    void Start()
    {
        // Preload a number of tiles into the pool
        PreloadTiles(10);
    }

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

    void PreloadTiles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            InstantiateTile();
        }
    }

    void SpawnTile()
    {
        GameObject tile = GetPooledTile();
        if (tile == null)
        {
            // If the pool is empty, instantiate a new tile
            InstantiateTile();
            return;
        }

        // Set the position and parent
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-4, -9));
        tile.transform.position = spawnPos;
        //tile.transform.SetParent(tilesMove.transform);

        // Activate the tile (if it has any deactivation logic)
        tile.SetActive(true);
    }

    void InstantiateTile()
    {
        tilePrefabReference.InstantiateAsync(tilesMove.transform.position, Quaternion.identity)
            .Completed += HandleTileInstantiation;
    }

    void HandleTileInstantiation(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject tile = handle.Result;
            tile.SetActive(false); // Deactivate the tile initially
            tilePool.Enqueue(tile);
        }
        else
        {
            Debug.LogError("Tile instantiation failed.");
        }
    }

    GameObject GetPooledTile()
    {
        if (tilePool.Count > 0)
        {
            // If there are pooled tiles, return one from the pool
            GameObject tile = tilePool.Dequeue();
            return tile;
        }

        return null;
    }
}
