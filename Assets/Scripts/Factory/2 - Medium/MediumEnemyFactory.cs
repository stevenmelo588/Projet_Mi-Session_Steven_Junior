using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor;
using Unity.Collections;

public class MediumEnemyFactory : MonoBehaviour, IAbstractEnemyFactory
{
    private static MediumEnemyFactory instance;
    public static MediumEnemyFactory Instance { get { return instance; } }

    int WeakEnemyIndex, StrongEnemyIndex;

    [SerializeField] GameObject[] mediumEnemyOBJ;

    //private int TotalEnemyCount => GameManager.Intance.totalEnemyCount;

    // [SerializeField] private GameObject weakEnemy; 
    // [SerializeField] private GameObject strongEnemy;

    // [SerializeField] int WeakEnemyAmount;
    // [SerializeField] int StrongEnemyAmount;

    [SerializeField] List<GameObject> pooledWeakEnemies;
    [SerializeField] List<GameObject> pooledStrongEnemies;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != this)
            instance = this;

        // if(Instance != null && Instance != this)
        //     Destroy(this.gameObject);
        // else
        //     Instance = this;

        //TotalEnemyCount = GameManager.instance.totalEnemyCount;
        PopulateEnemyPool();
    }

    private void Start()
    {
    }

    public void PopulateEnemyPool()
    {
        pooledWeakEnemies = new List<GameObject>();
        pooledStrongEnemies = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < GameManager.Instance.totalEnemyCount / 4; i++)
        {
            for (int j = 0; j < 1; j++) // instanciates 1 Strong Enemy for 3 Weak Enemies
            {
                tmp = Instantiate(mediumEnemyOBJ[1], this.transform);
                tmp.SetActive(false);
                pooledStrongEnemies.Add(tmp);

                for (int k = 0; k < 3; k++)
                {
                    tmp = Instantiate(mediumEnemyOBJ[0], this.transform);
                    tmp.SetActive(false);
                    pooledWeakEnemies.Add(tmp);
                }                
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
    //         tmp = Instantiate(mediumEnemyOBJs[1]);
    //         tmp.SetActive(false);
    //         pooledStrongEnemies.Add(tmp);

    //         for (int j = 0; j < 3; j++)
    //         {
    //             tmp = Instantiate(mediumEnemyOBJs[0]);
    //             tmp.SetActive(false);
    //             pooledWeakEnemies.Add(tmp);
    //         }
    //     }

    //     for (int i = 0; i < WeakEnemyAmount; i++)
    //     {
    //         tmp = Instantiate(mediumEnemyOBJs[0]);
    //         tmp.SetActive(false);
    //         pooledWeakEnemies.Add(tmp);
    //     }

    //     for (int j = 0; j < StrongEnemyAmount; j++)
    //     {
    //         tmp = Instantiate(mediumEnemyOBJs[1]);
    //         tmp.SetActive(false);
    //         pooledStrongEnemies.Add(tmp);
    //     }

    //     //GameObject tmp;

    // }

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
        //Destroy(strongEnemyPrefab);
        StrongEnemyIndex %= pooledStrongEnemies.Count;
        GameObject strongEnemy = pooledStrongEnemies[StrongEnemyIndex++];
        strongEnemy.SetActive(true);
        //pooledStrongEnemies[EnemyIndex++].SetActive(true);
        // Debug.Log("[ " + strongEnemy.transform.position + " ]");
        // print("Medium");
        return strongEnemy;
    }

}