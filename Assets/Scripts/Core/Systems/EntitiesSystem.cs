using Core.Systems.Base;
using Core.Systems.Interface;
using JetBrains.Annotations;
using Model.Data.EntityPool;
using Model.Data.Pointer;
using Model.Data.State;
using Model.Data.Unity.Config;
using Model.Entity;

namespace Core.Systems {
    [UsedImplicitly]
    public class EntitiesSystem : SystemBase, IEntitiesSystem {

        /// <summary>
        /// Create managers (pools) for entities and active entities lists
        /// </summary>
        public EntitiesSystem(ActiveEntitiesState active, EntitiesManagersState managers, PoolsConfig poolsConfig,
            BulletConfig bulletConfig, LaserConfig laserConfig,
            ObjectPointers<AsteroidConfig, AsteroidConfig.Size> asteroidConfigs, UfoConfig ufoConfig) {

            // Managers
            managers.ammo1 = new EntitiesManager<Bullet, BulletAmmoState, BulletConfig>(bulletConfig, poolsConfig.ammo1Capacity, out active.ammo1);
            managers.ammo2 = new EntitiesManager<Laser, LaserAmmoState, LaserConfig>(laserConfig, poolsConfig.ammo2Capacity, out active.ammo2);
            managers.ufos = new EntitiesManager<Ufo, UfoState, UfoConfig>(ufoConfig, poolsConfig.ufosCapacity, out active.ufos);
            managers.asteroidsManagers.Add(AsteroidConfig.Size.Large,
                new EntitiesManager<Asteroid, AsteroidState, AsteroidConfig>(
                    asteroidConfigs.Get(AsteroidConfig.Size.Large),
                    poolsConfig.asteroidsLargeCapacity, out IEntitiesList<Asteroid> activeAsteroidsLarge));
            managers.asteroidsManagers.Add(AsteroidConfig.Size.Medium,
                new EntitiesManager<Asteroid, AsteroidState, AsteroidConfig>(
                    asteroidConfigs.Get(AsteroidConfig.Size.Medium),
                    poolsConfig.asteroidsMediumCapacity, out IEntitiesList<Asteroid> activeAsteroidsMedium));
            managers.asteroidsManagers.Add(AsteroidConfig.Size.Small,
                new EntitiesManager<Asteroid, AsteroidState, AsteroidConfig>(
                    asteroidConfigs.Get(AsteroidConfig.Size.Small),
                    poolsConfig.asteroidsSmallCapacity, out IEntitiesList<Asteroid> activeAsteroidsSmall));


            // Active entities (only for Dictionaries)
            active.asteroidsDict.Add(AsteroidConfig.Size.Large, activeAsteroidsLarge);
            active.asteroidsDict.Add(AsteroidConfig.Size.Medium, activeAsteroidsMedium);
            active.asteroidsDict.Add(AsteroidConfig.Size.Small, activeAsteroidsSmall);
        }
    }
}