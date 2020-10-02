using System;
using System.Collections.Generic;

namespace Asteroids
{
    public static class AsteroidsPool
    {
        private static readonly List<AsteroidBehaviour> AsteroidList = new List<AsteroidBehaviour>();

        public static event Action OnAllAsteroidDead;

        public static event Action<int> OnAsteroidDead;

        private static float _increaseSpeedValue = 10f;

        public static void CreateAsteroid(AsteroidBehaviour asteroid)
        {
            AsteroidList.Add(asteroid);
            asteroid.IncreaseAsteroidSpeed(_increaseSpeedValue);
        }
    
        public static void DeleteAsteroid(AsteroidBehaviour asteroid)
        {
            OnAsteroidDead?.Invoke(asteroid.PointsForDestroy);
            AsteroidList.Remove(asteroid);
            if (AsteroidList.Count > 0) return;
            OnAllAsteroidDead?.Invoke();
            _increaseSpeedValue += 10;
        }
    }
}