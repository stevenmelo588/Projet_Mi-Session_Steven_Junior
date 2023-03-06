using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemyFactory : MonoBehaviour, IAbstractEnemyFactory, IPrototype
{
    //private static EasyEnemyFactory instance;
    public static EasyEnemyFactory Instance { get; private set; }

    int WeakEnemyIndex, StrongEnemyIndex;

    [SerializeField] GameObject[] easyEnemyOBJ;

    private int TotalEnemyCount => EnemySpawner.Instance.TotalEnemyCount;
    private int EnemySpawnRate => EnemySpawner.Instance.enemySpawnRateAmount;

    //[SerializeField] List<GameObject> pooledEasyEnemies;

    [SerializeField] List<GameObject> pooledWeakEnemies;
    [SerializeField] List<GameObject> pooledStrongEnemies;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        //pooledEasyEnemies = new List<GameObject>();
        PopulateEnemyPool();

        //if (instance != this)

        // if(Instance != null && Instance != this)
        // Destroy(this.gameObject);
        // else

        //totalEnemyCount = GameManager.instance.totalEnemyCount;
    }

    public void PopulateEnemyPool()
    {
        pooledWeakEnemies = new List<GameObject>();
        pooledStrongEnemies = new List<GameObject>();
        //GameObject tmp;

        for (int i = 0; i < TotalEnemyCount / EnemySpawnRate; i++)
        {
            for (int j = 0; j < (EnemySpawnRate - (EnemySpawnRate - 1)); j++) // instanciates 1 Strong Enemy for 3 Weak Enemies
            {
                //tmp = Instantiate(easyEnemyOBJ[1], this.transform);
                //tmp = Clone(easyEnemyOBJ[1]);
                //tmp.SetActive(false);

                for (int k = 0; k < (EnemySpawnRate - 1); k++)
                {
                    //tmp = Instantiate(easyEnemyOBJ[0], this.transform);
                    //tmp = Clone(easyEnemyOBJ[0]);
                    //tmp.SetActive(false);
                    pooledWeakEnemies.Add(Clone(easyEnemyOBJ[0]));
                }

                pooledStrongEnemies.Add(Clone(easyEnemyOBJ[1]));
            }
        }
    }

    public GameObject CreateWeakEnemy()
    {
        //Debug.Log(WeakEnemyIndex);
        pooledWeakEnemies[WeakEnemyIndex++ % pooledWeakEnemies.Count].SetActive(true);

        return pooledWeakEnemies[WeakEnemyIndex];

        //WeakEnemyIndex %= pooledWeakEnemies.Count;
        //GameObject weakEnemy = pooledWeakEnemies[WeakEnemyIndex++];
        //weakEnemy.SetActive(true);

        //return weakEnemy;
    }

    public GameObject CreateStrongEnemy()
    {
        //Debug.Log(StrongEnemyIndex);
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
