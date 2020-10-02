using UnityEngine;
using static AsteroidsPool;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MovementComponent), typeof(HealthComponent))]
public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private int pointsForDestroy;
    [SerializeField] private int asteroidSpawnCount = 2;
    [SerializeField] private int increasePointValue = 50;
    [SerializeField] private int asteroidLevel = 2;

    private MovementComponent _movement;
    private HealthComponent _health;

    public int PointsForDestroy => pointsForDestroy;

    public void IncreaseAsteroidSpeed(float valueSpeed) => _movement.IncreaseSpeed(valueSpeed);
    
    private void Awake()
    {
        _movement = GetComponent<MovementComponent>();
        _health = GetComponent<HealthComponent>();
        if(asteroidLevel > 0)
            _health.OnObjectDestroy += SpawnAsteroidsAfterDestroy;
        _movement.ChooseDirection(Camera.main.transform.position - transform.position);
        CreateAsteroid(this);
    }

    private void SpawnAsteroidsAfterDestroy()
    {
        for (int i = 0; i < asteroidSpawnCount; i++)
            SpawnSmallerAsteroid();
    }

    private void SpawnSmallerAsteroid()
    {
        var asteroid = Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
        asteroid.transform.localScale = gameObject.transform.localScale / 2;
        asteroid.GetComponent<MovementComponent>().ChooseDirection(RandomDirection(_movement.Direction));
        var asteroidBehaviour = asteroid.GetComponent<AsteroidBehaviour>();
        asteroidBehaviour.asteroidLevel = asteroidLevel - 1;
        asteroidBehaviour.pointsForDestroy = pointsForDestroy + increasePointValue;
    }

    private Vector2 RandomDirection(Vector2 direction)
    {
        return new Vector2(Random.Range(direction.y, -direction.y), Random.Range(-direction.x, direction.x));
    }

    private void OnDestroy()
    {
        _health.OnObjectDestroy -= SpawnSmallerAsteroid;
        DeleteAsteroid(this);
    }
    
}