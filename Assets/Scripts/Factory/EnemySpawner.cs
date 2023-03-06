//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbstractEnemyFactory
{
    public abstract void PopulateEnemyPool();
    public abstract GameObject CreateWeakEnemy();
    public abstract GameObject CreateStrongEnemy();
}

[DisallowMultipleComponent]
public class EnemySpawner : MonoBehaviour
{
    private float _SpawnRadius = 10f;

    private static EnemySpawner instance = null;
    public static EnemySpawner Instance { get => (instance == null) ? instance = FindObjectOfType<EnemySpawner>() : instance; }
    
    public int TotalEnemyCount = 100;
        //= GameManager.Instance.totalEnemyCount;

    private const int MAX_ENEMY_COUNT = 300;
    public static int enemyCount = 0;

    public int enemySpawnRateAmount = 4;

    IAbstractEnemyFactory factory;

    [SerializeField] LayerMask _WhatIsGround;

    private void Awake()
    {
        enemyCount = 0;
        StartCoroutine(ChangeFactoryCoroutine());
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator ChangeFactoryCoroutine()
    {
        while (true) 
        {
            Debug.Log("Easy");
            factory = EasyEnemyFactory.Instance;
            yield return new WaitForSeconds(60); // Waits 1 minute before changing factory
            Debug.Log("Medium");
            factory = MediumEnemyFactory.Instance;
            yield return new WaitForSeconds(60); // Waits 1 minute before changing factory
            Debug.Log("Hard");
            factory = HardEnemyFactory.Instance;
            yield return new WaitForSeconds(60); // Waits 1 minute before changing factory
        }
    }

    Vector2 RandomPositionAroundPlayer() => (Vector2)GameManager.Instance.Player.transform.position + (Random.insideUnitCircle * _SpawnRadius);
    // Vector3 RandomPositionAroundPlayer() => player.transform.position + (Random.insideUnitSphere * _SpawnRadius);

    IEnumerator SpawnCoroutine()
    {
        // Vector2 position = myFunc();
        while (enemyCount < MAX_ENEMY_COUNT)
        {
            for (int i = 0; i < enemySpawnRateAmount; i++) // Default i = 4;
            {
                // (i % 4 == 4) (i > 0)
                var enemy = (i > 0) ? factory.CreateWeakEnemy() : factory.CreateStrongEnemy();
                enemy.transform.position = RandomPositionAroundPlayer();

                //if (enemy.CompareTag("NoSpawn"))
                //    enemy.transform.position = RandomPositionAroundPlayer();

                //enemyCount += 4;
                enemyCount++;
            }
            yield return new WaitForSeconds(3f); //Default 15f
        }
    }
}
