using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject tilesMove;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] float spawnRate;
    float nextSpawnTime = 0f;

    // Object Pooling için kullanılacak liste
    List<GameObject> tilePool = new();

    void Start()
    {
        // İlk başta nesneleri oluşturup havuzda saklayalım
        for (int i = 0; i < 6; i++)
        {
            GameObject tile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
            tile.SetActive(false);
            tilePool.Add(tile);
        }
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

    void SpawnTile()
    {
        // Havuzdan bir nesne al
        GameObject tile = GetPooledTile();

        if (tile != null)
        {
            // Nesneyi yerine koy
            Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-4, -9));
            tile.transform.position = spawnPos;
            tile.SetActive(true);

            // Hareketi sağlamak için parent'ını belirle
            tile.transform.SetParent(tilesMove.transform);
        }
    }

    GameObject GetPooledTile()
    {
        // Havuzdaki kullanılmayan bir nesneyi bul
        for (int i = 0; i < tilePool.Count; i++)
        {
            if (!tilePool[i].activeInHierarchy)
            {
                return tilePool[i];
            }
        }

        // Havuzdaki tüm nesneler kullanılıyorsa yeni bir nesne oluştur ve havuza ekle
        GameObject newTile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
        newTile.SetActive(false);
        tilePool.Add(newTile);

        return newTile;
    }

}
