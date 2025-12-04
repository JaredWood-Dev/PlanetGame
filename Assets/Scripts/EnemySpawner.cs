using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    public float timeBetweenSpawns;
    public float spawnAmount;
    public Transform player;
    
    public GameManager gameManager;

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
                var enemyHealth = spawnedEnemy.GetComponent<Health>();
                enemyHealth.maxHealth += (1 * gameManager.difficulty);
                var enemyComponent = spawnedEnemy.GetComponent<EnemyController>();
                enemyComponent.player = player;
                enemyComponent.damage += (1 * (int)gameManager.difficulty);
            }
        }
    }
}
