using System.Collections;
using UnityEngine;

public class GoodSpawner1 : MonoBehaviour
{
    void Start() => StartCoroutine(SpawnCoroutine());

    IEnumerator SpawnCoroutine()
    {
        //yield return new WaitForSeconds(5);

        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                EnemyFactory.Instance.CreateWeakEnemy().transform.position = RandomPosition();
            }
            EnemyFactory.Instance.CreateStrongEnemy().transform.position = RandomPosition();
            yield return new WaitForSeconds(5);
        }
    }

    Vector3 RandomPosition() => transform.position + Random.insideUnitSphere;
}
