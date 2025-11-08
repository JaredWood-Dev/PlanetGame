using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    public float timeBetweenSpawns;
    public float spawnAmount;

    [Serializable]
    public struct Volume
    {
        public Vector2 TopRight;
        public Vector2 BottomLeft;
    }

    [SerializeField]
    public Volume spawnBound;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", timeBetweenSpawns, timeBetweenSpawns);
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < spawnAmount + (Random.Range(-3, 3)); i++)
        {
            foreach (var enemy in enemies)
            {
                var spawnedEnemy = Instantiate(enemy);
                spawnedEnemy.transform.position = new Vector2(Random.Range(spawnBound.BottomLeft.x, spawnBound.TopRight.x), Random.Range(spawnBound.BottomLeft.y, spawnBound.TopRight.y));
            }
        }
    }
}
