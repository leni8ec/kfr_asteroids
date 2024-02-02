using Core.Config;
using UnityEngine;

namespace Core.Unity {
    public class ConfigCollector : MonoBehaviour {
        public PlayerConfig player;
        public BulletConfig bullet;
        public LaserConfig laser;

        [Space]
        public AsteroidConfig asteroidLarge;
        public AsteroidConfig asteroidMedium;
        public AsteroidConfig asteroidSmall;

        [Space]
        public UfoConfig ufo;

        [Space]
        public WorldConfig world;

        [Space]
        public SoundsConfig sounds;

    }
}