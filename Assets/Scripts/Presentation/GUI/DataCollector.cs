using Presentation.Data;
using UnityEngine;

namespace Presentation.GUI {
    public class DataCollector : MonoBehaviour {
        public PlayerData playerData;
        public BulletData bulletData;
        public LaserData laserData;
        [Space]
        public AsteroidData asteroidData;
        public UfoData ufoData;
        [Space]
        public WorldData worldData;
    }
}