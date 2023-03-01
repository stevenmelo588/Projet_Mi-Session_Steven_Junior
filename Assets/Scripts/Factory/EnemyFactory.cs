using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance { get; private set; }

    [SerializeField] GameObject[] enemyOBJ;

    //[SerializeField] GameObject weakEnemyOBJ;
    //[SerializeField] GameObject strongEnemyOBJ;

    [SerializeField] int WeakEnemyAmount;
    [SerializeField] int StrongEnemyAmount;

    int WeakEnemyIndex, StrongEnemyIndex;
    [SerializeField] List<GameObject> pooledWeakEnemies;
    [SerializeField] List<GameObject> pooledStrongEnemies;

    private void Awake()
    {
        PopulateEnemyPool();

        //PopulateWeakEnemyPool();
        //PopulateStrongEnemyPool();

        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //weakEnemyPrefab = GetComponent<GameObject>();
        //strongEnemyPrefab = GetComponent<GameObject>();
    }

    void PopulateWeakEnemyPool()
    {
        pooledWeakEnemies = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < WeakEnemyAmount; i++)
        {
            tmp = Instantiate(enemyOBJ[0]);
            tmp.SetActive(false);
            pooledWeakEnemies.Add(tmp);
        }
    }
    void PopulateStrongEnemyPool()
    {
        pooledStrongEnemies = new List<GameObject>();
        GameObject tmp;
        for (int j = 0; j < StrongEnemyAmount; j++)
        {
            tmp = Instantiate(enemyOBJ[1]);
            tmp.SetActive(false);
            pooledStrongEnemies.Add(tmp);
        }
    }

    void PopulateEnemyPool()
    {
        pooledWeakEnemies = new List<GameObject>();
        pooledStrongEnemies = new List<GameObject>();
        GameObject tmp;

        // for (int i = 0; i < length; i++)
        // {

        // }
        for (int i = 0; i < (WeakEnemyAmount + StrongEnemyAmount) / 4; i++)
        {
            for (int j = 0; j < 1; j++)
            {

                for (int k = 0; k < 3; k++)
                {
                    tmp = Instantiate(enemyOBJ[0], this.transform);
                    tmp.SetActive(false);
                    pooledWeakEnemies.Add(tmp);
                }
                
                tmp = Instantiate(enemyOBJ[1], this.transform);
                tmp.SetActive(false);
                pooledStrongEnemies.Add(tmp);
            }
        }

        // for (int i = 0; i < WeakEnemyAmount; i++)
        // {
        //     tmp = Instantiate(enemyOBJ[0]);
        //     tmp.SetActive(false);
        //     pooledWeakEnemies.Add(tmp);
        // }

        // for (int j = 0; j < StrongEnemyAmount; j++)
        // {
        //     tmp = Instantiate(enemyOBJ[1]);
        //     tmp.SetActive(false);
        //     pooledStrongEnemies.Add(tmp);
        // }

        //GameObject tmp;

    }

    // Update is called once per frame
    void Update()
    {

    }

    //private GameObject GetWeakPoolOBJ()
    //{
    //    return ;
    //}

    public GameObject CreateWeakEnemy()
    {
        WeakEnemyIndex %= pooledWeakEnemies.Count;
        GameObject weakEnemy = pooledWeakEnemies[WeakEnemyIndex++];
        weakEnemy.SetActive(true);

        foreach (GameObject item in FindObjectsOfType(weakEnemy.GetType()))
        {
            if (item.name.Equals(weakEnemy.name + "(Clone)"))
                if (item.activeInHierarchy)
                    Debug.Log(item.name);
        }
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
        Debug.Log("[ " + strongEnemy.transform.position + " ]");
        return strongEnemy;
    }
}
