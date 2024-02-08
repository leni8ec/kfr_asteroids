using System.Collections.Generic;
using Model.Core.Objects.Game;
using Model.Core.Pools;
using Model.Core.Unity.Data.Config;
using UnityEngine;

namespace Model.Core.Data.State {
    public class ObjectsState {

        // Base
        public Camera camera;

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