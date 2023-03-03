using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemyFactory : MonoBehaviour, IAbstractEnemyFactory
{
    //private static EasyEnemyFactory instance;
    public static EasyEnemyFactory Instance { get; private set; }

    int WeakEnemyIndex, StrongEnemyIndex;

    [SerializeField] GameObject[] easyEnemyOBJ;

    // [SerializeField] private GameObject weakEnemy; 
    // [SerializeField] private GameObject strongEnemy;

    private int TotalEnemyCount => GameManager.Instance.totalEnemyCount;

    // [SerializeField] int WeakEnemyAmount;
    // [SerializeField] int StrongEnemyAmount;

    [SerializeField] List<GameObject> pooledWeakEnemies;
    [SerializeField] List<GameObject> pooledStrongEnemies;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        PopulateEnemyPool();
        //if (instance != this)

        // if(Instance != null && Instance != this)
        // Destroy(this.gameObject);
        // else

        //totalEnemyCount = GameManager.instance.totalEnemyCount;
    }

    //private void Start()
    //{
    //}

    public void PopulateEnemyPool()
    {
        pooledWeakEnemies = new List<GameObject>();
        pooledStrongEnemies = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < GameManager.Instance.totalEnemyCount / 4; i++)
        {
            for (int j = 0; j < 1; j++) // instanciates 1 Strong Enemy for 3 Weak Enemies
            {
                tmp = Instantiate(easyEnemyOBJ[1], this.transform);
                tmp.SetActive(false);
                pooledStrongEnemies.Add(tmp);

                for (int k = 0; k < 3; k++)
                {
                    tmp = Instantiate(easyEnemyOBJ[0], this.transform);
                    tmp.SetActive(false);
                    pooledWeakEnemies.Add(tmp);
                }
            }
        }
    }

    public GameObject CreateWeakEnemy()
    {
        WeakEnemyIndex %= pooledWeakEnemies.Count;
        GameObject weakEnemy = pooledWeakEnemies[WeakEnemyIndex++];
        weakEnemy.SetActive(true);

        // foreach (GameObject item in FindObjectsOfType(weakEnemy.GetType()))
        // {
        //     if (item.name.Equals(weakEnemy.name + "(Clone)"))
        //         if (item.activeInHierarchy)
        //             Debug.Log(item.name);
        // }
        //;

        return weakEnemy;
    }

    public GameObject CreateStrongEnemy()
    {
        print(pooledStrongEnemies.Count);
        //Destroy(strongEnemyPrefab);
        StrongEnemyIndex %= pooledStrongEnemies.Count;
        GameObject strongEnemy = pooledStrongEnemies[StrongEnemyIndex++];
        strongEnemy.SetActive(true);
        //pooledStrongEnemies[EnemyIndex++].SetActive(true);
        // Debug.Log("[ " + strongEnemy.transform.position + " ]");
        // print("Easy");
        return strongEnemy;
    }
}
