using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MovementComponent), typeof(HealthComponent))]
public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    
    private MovementComponent _movement;
    private HealthComponent _health;
    private int _asteroidLevel = 2;

    private const int AsteroidSpawnCount = 2;

    private void Start()
    {
        _movement = GetComponent<MovementComponent>();
        _health = GetComponent<HealthComponent>();
        if(_asteroidLevel > 0)
            _health.OnObjectDestroy += SpawnAsteroidsAfterDestroy;
        _movement.ChooseDirection(Camera.main.transform.position - transform.position);
    }

    private void SpawnAsteroidsAfterDestroy()
    {
        for (int i = 0; i < AsteroidSpawnCount; i++)
            SpawnSmallerAsteroid();
    }

    private void SpawnSmallerAsteroid()
    {
        var asteroid = Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
        asteroid.transform.localScale = gameObject.transform.localScale / 2;
        asteroid.GetComponent<MovementComponent>().ChooseDirection(RandomDirection(_movement.Direction));
        asteroid.GetComponent<AsteroidBehaviour>()._asteroidLevel = _asteroidLevel - 1;
    }

    private Vector2 RandomDirection(Vector2 direction)
    {
        return new Vector2(Random.Range(direction.y, -direction.y), Random.Range(-direction.x, direction.x));
    }

    private void OnDestroy()
    {
        _health.OnObjectDestroy -= SpawnSmallerAsteroid;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<InputComponent>() == null) return;
        other.GetComponent<HealthComponent>().ChangeHealth(-1);
        Destroy(gameObject);
    }
}