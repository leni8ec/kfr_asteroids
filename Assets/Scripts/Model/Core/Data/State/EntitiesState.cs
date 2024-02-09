using System.Collections.Generic;
using Model.Core.Entity;
using Model.Core.Pools;
using Model.Core.Unity.Data.Config;

namespace Model.Core.Data.State {
    public class EntitiesState {

        // Player
        public Player player;

        // Weapon
        public BulletPool ammo1Pool;
        public LaserPool ammo2Pool;

        // World
        public UfoPool ufosPool;
        public Dictionary<AsteroidConfig.Size, AsteroidPool> asteroidPools;

    }
}