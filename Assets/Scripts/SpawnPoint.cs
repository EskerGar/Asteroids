using System;
using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnDelay;

    private void Start()
    {
        StartCoroutine(StartSpawn(prefab, spawnDelay));
    }


    private IEnumerator StartSpawn(GameObject enemy, float delay)
    {
        while (true)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(delay);
        }
    }
    
    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}