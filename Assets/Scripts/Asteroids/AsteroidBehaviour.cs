using Components;
using UnityEngine;
using static Asteroids.AsteroidsPool;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(MovementComponent), typeof(HealthComponent), typeof(SpriteRenderer))]
    public class AsteroidBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject asteroidPrefab;
        [SerializeField] private AsteroidConfigs asteroidConfigs;
        [SerializeField] private int pointsForDestroy;
        [SerializeField] private int asteroidLevel = 2;

        private MovementComponent _movement;
        private HealthComponent _health;
        private SpriteRenderer _spriteRenderer;

        public int PointsForDestroy => pointsForDestroy;

        public void IncreaseAsteroidSpeed(float valueSpeed) => _movement.IncreaseSpeed(valueSpeed);
    
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _movement = GetComponent<MovementComponent>();
            _health = GetComponent<HealthComponent>();
            SetNewSprite(asteroidConfigs.SpritesList[asteroidLevel + 1]);
            if(asteroidLevel > 0)
                _health.OnObjectDestroy += SpawnAsteroidsAfterDestroy;
            _health.OnObjectDestroy += WhenAsteroidDestroy;
            _movement.ChooseDirection(Camera.main.transform.position - transform.position);
            CreateAsteroid(this);
        }

        private void SpawnAsteroidsAfterDestroy()
        {
            for (int i = 0; i < asteroidConfigs.AsteroidSpawnCount; i++)
                SpawnSmallerAsteroid();
        }

        private void SetNewSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

        private void SpawnSmallerAsteroid()
        {
            var asteroid = Instantiate(asteroidPrefab, transform.position, Quaternion.identity);
            asteroid.transform.localScale = gameObject.transform.localScale / 2;
            asteroid.GetComponent<MovementComponent>().ChooseDirection(RandomDirection(_movement.Direction));
            var asteroidBehaviour = asteroid.GetComponent<AsteroidBehaviour>();
            asteroidBehaviour.asteroidLevel = asteroidLevel - 1;
            asteroidBehaviour.SetNewSprite(asteroidConfigs.SpritesList[asteroidBehaviour.asteroidLevel + 1]);
            asteroidBehaviour.pointsForDestroy = pointsForDestroy + asteroidConfigs.IncreasePointValue;
        }

        private Vector2 RandomDirection(Vector2 direction)
        {
            return new Vector2(Random.Range(direction.y, -direction.y), Random.Range(-direction.x, direction.x));
        }

        private void WhenAsteroidDestroy()
        {
            _health.OnObjectDestroy -= SpawnSmallerAsteroid;
            DeleteAsteroid(this);
        }
    
    }
}