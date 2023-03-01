using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbstractEnemyFactory
{
    public abstract void PopulateEnemyPool();
    public abstract GameObject CreateWeakEnemy();
    public abstract GameObject CreateStrongEnemy();
}

public class EnemySpawner : MonoBehaviour
{
    private float _SpawnRadius = 1.5f;
    //public static EnemySpawner Instance { get; private set; }

    //public static int TotalEnemyCount => GameManager.instance.totalEnemyCount;
     
    //[SerializeField] private int totalEnemyCount = 100;
    //public int totalEnemyCount = 100;
     
    private const int MAX_ENEMY_COUNT = 300;
    public static int enemyCount = 0;

    //[SerializeField] private GameObject player;

    //public GameObject Player { get; private set; }
    IAbstractEnemyFactory factory;

    //public int TotalEnemyCount { get => totalEnemyCount; set => totalEnemyCount = 100; }
    //public GameObject Player { get => this.player; set => this.player = value; }/

    private void Awake()
    {
        //if (Instance != null && Instance != this)
        //    Destroy(this.gameObject);
        //else
        //    Instance = this;

        //EasyEnemyFactory.Instance.PopulateEnemyPool();
        //MediumEnemyFactory.Instance.PopulateEnemyPool();
        //HardEnemyFactory.Instance.PopulateEnemyPool();

        //TotalEnemyCount = totalEnemyCount;

        //player = GameObject.FindGameObjectWithTag("Player");

        //EasyEnemyFactory.Instance.TotalEnemyCount = TotalEnemyCount;
        //MediumEnemyFactory.Instance.TotalEnemyCount = TotalEnemyCount;
        //HardEnemyFactory.Instance.TotalEnemyCount = TotalEnemyCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeFactoryCoroutine());
        // player = GameObject.FindGameObjectWithTag("Player");

        // EasyEnemyFactory.Instance.TotalEnemyCount = TotalEnemyCount;
        // MediumEnemyFactory.Instance.TotalEnemyCount = TotalEnemyCount;
        // HardEnemyFactory.Instance.TotalEnemyCount = TotalEnemyCount;

        StartCoroutine(SpawnCoroutine());

        //factory.PopulateEnemyPool();
        // factory.CreateStrongEnemy();
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

    // private void OnDrawGizmosSelected() {
    //     Gizmos.DrawWireSphere(player.transform.position, player.GetComponent<CircleCollider2D>().radius);

    //     // RandomPositionAroundPlayer();
    // }

    // private void OnDrawGizmosSelected() {
    // }
    // .normalized
    Vector2 RandomPositionAroundPlayer() => (Vector2)GameManager.Instance.Player.transform.position + (Random.insideUnitCircle * _SpawnRadius);
    // Vector3 RandomPositionAroundPlayer() => player.transform.position + (Random.insideUnitSphere * _SpawnRadius);

    IEnumerator SpawnCoroutine()
    {
        while (enemyCount < MAX_ENEMY_COUNT)
        {
            for (int i = 0; i < 4; i++) // Default i = 4;
            {
                // (i % 4 == 4)
                var enemy = (i > 0) ? factory.CreateWeakEnemy() : factory.CreateStrongEnemy();
                // Vector3 randOffset = Random.insideUnitSphere * 5;
                // player.transform.position + randOffset.normalized;
                enemy.transform.position = RandomPositionAroundPlayer();
                enemyCount += 4;
            }
            yield return new WaitForSeconds(5f); //Default 15f
        }
    }
}
