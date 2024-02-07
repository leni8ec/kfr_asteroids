﻿using System.Collections.Generic;
using Model.Core.Config;
using Model.Core.Objects;
using Model.Core.Pools;
using UnityEngine;

namespace Model.Core.State {
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