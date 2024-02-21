using System.Collections.Generic;
using JetBrains.Annotations;
using Model.Core.Container.Object;
using Model.Core.Data.State;
using Model.Core.Pools;
using Model.Core.Unity.Data.Config;
using Model.Domain.Systems.Base;
using Model.Domain.Systems.Interface;

namespace Model.Domain.Systems {
    [UsedImplicitly]
    public class EntityPoolSystem : SystemBase, IEntityPoolSystem {

        public EntityPoolSystem(EntitiesState entities, PoolsConfig config, BulletConfig bulletConfig, LaserConfig laserConfig,
            ObjectPointers<AsteroidConfig, AsteroidConfig.Size> asteroidConfigs, UfoConfig ufoConfig) {

            // Ammo (for weapons)
            entities.ammo1Pool = new BulletPool(bulletConfig, config.ammo1Capacity);
            entities.ammo2Pool = new LaserPool(laserConfig, config.ammo2Capacity);

            // World
            entities.ufosPool = new UfoPool(ufoConfig, config.ufosCapacity);
            entities.asteroidPools = new Dictionary<AsteroidConfig.Size, AsteroidPool> {
                { AsteroidConfig.Size.Large, new AsteroidPool(asteroidConfigs.Get(AsteroidConfig.Size.Large), config.asteroidsLargeCapacity) },
                { AsteroidConfig.Size.Medium, new AsteroidPool(asteroidConfigs.Get(AsteroidConfig.Size.Medium), config.asteroidsMiddleCapacity) },
                { AsteroidConfig.Size.Small, new AsteroidPool(asteroidConfigs.Get(AsteroidConfig.Size.Small), config.asteroidsSmallCapacity) }
            };

        }
    }
}