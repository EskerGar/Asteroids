using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnDelay;
    [SerializeField] private bool isPeriodicSpawn;

    private void Start()
    {
        if (isPeriodicSpawn)
            StartCoroutine(SpawnCoroutine());
        else
        {
            SpawnEnemy();
            AsteroidsPool.OnAllAsteroidDead += SpawnEnemy;
        }
    }


    private void SpawnEnemy()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        if(!isPeriodicSpawn)
            AsteroidsPool.OnAllAsteroidDead -= SpawnEnemy;
    }
    
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemy();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}