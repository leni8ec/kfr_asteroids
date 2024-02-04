using System.Collections.Generic;
using Core.Base;
using Core.Config;
using Core.Objects;
using UnityEngine;

namespace Core.State {
    public class ObjectsState {

        // Base
        public Camera camera;
        public Player player;

        // Player


        // Weapon
        public EntityPool<Bullet, BulletConfig> ammo1Pool;
        public EntityPool<Laser, LaserConfig> ammo2Pool;


        // World
        public EntityPool<Ufo, UfoConfig> ufosPool;
        public Dictionary<Asteroid.Size, EntityPool<Asteroid, AsteroidConfig>> asteroidPools;
    }
}