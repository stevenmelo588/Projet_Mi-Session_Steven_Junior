using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemyFactory : MonoBehaviour, IAbstractEnemyFactory, IPrototype
{
    //private static HardEnemyFactory instance;
    public static HardEnemyFactory Instance { get;  private set; }

    int WeakEnemyIndex, StrongEnemyIndex;

    [SerializeField] GameObject[] hardEnemyOBJ;

    private int TotalEnemyCount => EnemySpawner.Instance.TotalEnemyCount;
    private int EnemySpawnRate => EnemySpawner.Instance.enemySpawnRateAmount;

    [SerializeField] List<GameObject> pooledWeakEnemies;
    [SerializeField] List<GameObject> pooledStrongEnemies;

    // Start is called before the first frame update
    void Awake()
    {
        //if(instance != this)
        Instance = this;

        // if(Instance != null && Instance != this)
        //     Destroy(this.gameObject);
        // else
        //     Instance = this;

        //TotalEnemyCount = GameManager.instance.totalEnemyCount;
        PopulateEnemyPool();
    }

    //private void Start()
    //{
    //}

    public void PopulateEnemyPool()
    {
        pooledWeakEnemies = new List<GameObject>();
        pooledStrongEnemies = new List<GameObject>();
        //GameObject tmp;

        for (int i = 0; i < TotalEnemyCount / EnemySpawnRate; i++)
        {
            for (int j = 0; j < (EnemySpawnRate - (EnemySpawnRate - 1)); j++) // instanciates 1 Strong Enemy for 3 Weak Enemies
            {
                for (int k = 0; k < (EnemySpawnRate - 1); k++)
                {
                    //tmp = Instantiate(hardEnemyOBJ[0], this.transform);
                    //tmp.SetActive(false);
                    pooledWeakEnemies.Add(Clone(hardEnemyOBJ[0]));
                }

                //tmp = Instantiate(hardEnemyOBJ[1], this.transform);
                //tmp.SetActive(false);
                pooledStrongEnemies.Add(Clone(hardEnemyOBJ[1]));
            }
        }
    }

    // void PopulateEnemyPool()
    // {
    //     pooledWeakEnemies = new List<GameObject>();
    //     pooledStrongEnemies = new List<GameObject>();
    //     GameObject tmp;

    //     // for (int i = 0; i < length; i++)
    //     // {

    //     // }
    //     for (int i = 0; i < 1; i++)
    //     {
    //         tmp = Instantiate(enemyOBJ[1]);
    //         tmp.SetActive(false);
    //         pooledStrongEnemies.Add(tmp);

    //         for (int j = 0; j < 3; j++)
    //         {
    //             tmp = Instantiate(enemyOBJ[0]);
    //             tmp.SetActive(false);
    //             pooledWeakEnemies.Add(tmp);
    //         }
    //     }

    //     for (int i = 0; i < WeakEnemyAmount; i++)
    //     {
    //         tmp = Instantiate(enemyOBJ[0]);
    //         tmp.SetActive(false);
    //         pooledWeakEnemies.Add(tmp);
    //     }

    //     for (int j = 0; j < StrongEnemyAmount; j++)
    //     {
    //         tmp = Instantiate(enemyOBJ[1]);
    //         tmp.SetActive(false);
    //         pooledStrongEnemies.Add(tmp);
    //     }

    //     //GameObject tmp;

    // }

    public GameObject CreateWeakEnemy()
    {
        pooledWeakEnemies[WeakEnemyIndex++ % pooledWeakEnemies.Count].SetActive(true);

        return pooledWeakEnemies[WeakEnemyIndex];

        //WeakEnemyIndex %= pooledWeakEnemies.Count;
        //GameObject weakEnemy = pooledWeakEnemies[WeakEnemyIndex++];
        //weakEnemy.SetActive(true);

        //return weakEnemy;
    }

    public GameObject CreateStrongEnemy()
    {
        pooledStrongEnemies[StrongEnemyIndex++ % pooledStrongEnemies.Count].SetActive(true);

        return pooledStrongEnemies[StrongEnemyIndex];


        //StrongEnemyIndex %= pooledStrongEnemies.Count;
        //GameObject strongEnemy = pooledStrongEnemies[StrongEnemyIndex++];
        //strongEnemy.SetActive(true);

        //return strongEnemy;
    }

    public GameObject Clone(GameObject objectToClone)
    {
        objectToClone.SetActive(false);
        return Instantiate(objectToClone, this.transform);
    }
}
