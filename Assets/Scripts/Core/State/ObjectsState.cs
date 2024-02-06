using System.Collections.Generic;
using Core.Config;
using Core.Interface.Objects;
using Core.Objects;
using Core.Pools;
using UnityEngine;

namespace Core.State {
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