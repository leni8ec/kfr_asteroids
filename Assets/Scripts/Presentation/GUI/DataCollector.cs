using Presentation.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Presentation.GUI {
    public class DataCollector : MonoBehaviour {
        public PlayerData playerData;
        public BulletData bulletData;
        public LaserData laserData;
        [Space]
        public AsteroidData asteroidLargeData;
        public AsteroidData asteroidMediumData;
        public AsteroidData asteroidSmallData;
        [Space]
        public UfoData ufoData;
        [Space]
        public WorldData worldData;
        [Space]
        public SoundsData soundsData;
    }
}