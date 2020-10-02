using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Configs/AsteroidConfigs", fileName = "AsteroidConfigs")]
    public class AsteroidConfigs : ScriptableObject
    {
        [SerializeField] [Tooltip("Sort ascending size")] private List<Sprite> spritesList;
        [SerializeField] private int asteroidSpawnCount = 2;
        [SerializeField] private int increasePointValue = 50;

        public List<Sprite> SpritesList => spritesList;

        public int AsteroidSpawnCount => asteroidSpawnCount;

        public int IncreasePointValue => increasePointValue;
    }
}
