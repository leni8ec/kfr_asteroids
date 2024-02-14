using System.Collections.Generic;
using JetBrains.Annotations;
using Model.Core.Container.Object;
using Model.Core.Data.State;
using Model.Core.Data.State.Base;
using Model.Core.Entity;
using Model.Core.Entity.Base;
using Model.Core.Game;
using Model.Core.Interface.Adapters;
using Model.Core.Interface.Entity;
using Model.Core.Pools;
using Model.Core.Unity.Data.Config;
using Model.Domain.Systems.Base;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Model.Domain.Systems {
    [UsedImplicitly]
    public class WorldSystem : SystemBase, IUpdateSystem {
        private WorldSystemState State { get; }
        private WorldConfig Config { get; }

        private Player Player { get; }
        private ICameraAdapter Camera { get; }

        private BulletPool BulletPool { get; }

        private UfoPool UfoPool { get; }
        private Dictionary<AsteroidConfig.Size, AsteroidPool> AsteroidPools { get; }


        private ValueChange<GameStatus> GameStatus { get; }

        public WorldSystem(WorldConfig config, WorldSystemState state,
            ObjectPointers<AsteroidConfig, AsteroidConfig.Size> asteroidConfigs, UfoConfig ufoConfig,
            EntitiesState entities, GameSystemState gameSystemState, ICameraAdapter cameraAdapter) {

            Config = config;
            State = state;

            // Fill entities state
            entities.ufosPool = new UfoPool(ufoConfig);
            entities.asteroidPools = new Dictionary<AsteroidConfig.Size, AsteroidPool> {
                { AsteroidConfig.Size.Large, new AsteroidPool(asteroidConfigs.Get(AsteroidConfig.Size.Large)) },
                { AsteroidConfig.Size.Medium, new AsteroidPool(asteroidConfigs.Get(AsteroidConfig.Size.Medium)) },
                { AsteroidConfig.Size.Small, new AsteroidPool(asteroidConfigs.Get(AsteroidConfig.Size.Small)) }
            };


            // Link properties
            Camera = cameraAdapter;
            Player = entities.player;
            BulletPool = entities.ammo1Pool;
            UfoPool = entities.ufosPool;
            AsteroidPools = entities.asteroidPools;
            GameStatus = gameSystemState.Status;

            // Subscribe
            CollisionSystem.EnemyHitEvent += EnemyHitHandler;
            Asteroid.ExplosionEvent += AsteroidExplosionHandler;

            // Game state events
            GameStateSystem.NewGameEvent += Play;
            GameStateSystem.GameOverEvent += Reset;
        }

        private void Play() {
            Enable();

            State.asteroidSpawnCountdown = 1 / Config.asteroidsSpawnRate;
            State.ufoSpawnCountdown = 1 / Config.ufoSpawnRate;
        }

        private void Reset() {
            Disable();
            State.Reset();

            UfoPool.ForEachActive(Destroy);
            foreach (AsteroidPool asteroidPool in AsteroidPools.Values)
                asteroidPool.ForEachActive(Destroy);

            return;
            void Destroy(IEntity entity) => entity.Destroy();
        }

        public void Upd(float deltaTime) {
            // Spawn
            if ((State.asteroidSpawnCountdown -= deltaTime) <= 0) {
                // Check asteroids count limit
                int totalActiveAsteroids = AsteroidPools[AsteroidConfig.Size.Large].ActiveCount
                                           + AsteroidPools[AsteroidConfig.Size.Medium].ActiveCount
                                           + AsteroidPools[AsteroidConfig.Size.Small].ActiveCount;
                if (totalActiveAsteroids < Config.asteroidsLimit) {
                    State.asteroidSpawnCountdown = 1 / Config.asteroidsSpawnRate;
                    SpawnAsteroid();
                }
            }

            if ((State.ufoSpawnCountdown -= deltaTime) <= 0) {
                int totalActiveUfo = UfoPool.ActiveCount;
                if (totalActiveUfo < Config.ufosLimit) {
                    State.ufoSpawnCountdown = 1 / Config.ufoSpawnRate;
                    SpawnUfo();
                }
            }

            ProcessInfinityScreen();
        }


        private Rect GetWorldLimits(float screenOffset) {
            Vector2 min = Camera.ScreenToWorldPoint(Vector3.zero);
            Vector2 max = Camera.ScreenToWorldPoint(new Vector3(Camera.ScreenWidth, Camera.ScreenHeight));
            Rect limits = new(min.x - screenOffset, min.y - screenOffset, max.x - min.x + screenOffset * 2, max.y - min.y + screenOffset * 2);
            return limits;
        }

        private void ProcessInfinityScreen() {
            Rect worldBorders = GetWorldLimits(Config.screenInfinityOutsideOffset);
            void ProcessEntity(EntityBase entity) => ProcessEntityOutOfScreen(worldBorders, entity);

            // Player
            ProcessEntityOutOfScreen(worldBorders, Player);

            // Enemies
            UfoPool.ForEachActive(ProcessEntity);
            foreach (AsteroidPool asteroidPool in AsteroidPools.Values)
                asteroidPool.ForEachActive(ProcessEntity);

            // Bullets
            BulletPool.ForEachActive(ProcessEntity);
        }

        private void ProcessEntityOutOfScreen(Rect worldBorders, EntityBase entity) {
            Vector3 pos = entity.Transform.position;
            if (worldBorders.Contains(pos)) return;

            if (pos.x < worldBorders.x) pos.x = worldBorders.xMax;
            else if (pos.y < worldBorders.y) pos.y = worldBorders.yMax;
            else if (pos.x > worldBorders.xMax) pos.x = worldBorders.x;
            else if (pos.y > worldBorders.yMax) pos.y = worldBorders.y;

            entity.Transform.position = pos;
        }


        private void SpawnAsteroid() {
            Asteroid asteroid = AsteroidPools[AsteroidConfig.Size.Large].Take();
            Vector3 spawnPoint = GetRandomSpawnPoint();
            Vector3 direction = GetRandomDirection(spawnPoint);
            asteroid.Transform.position = spawnPoint;
            asteroid.Set(direction);
        }

        private void SpawnUfo() {
            Ufo ufo = UfoPool.Take();
            Vector3 spawnPoint = GetRandomSpawnPoint();
            Vector3 direction = GetRandomDirection(spawnPoint);
            ufo.Transform.position = spawnPoint;
            ufo.Set(direction);
            ufo.SetTarget(Player.Transform);
        }


        private Vector3 GetRandomSpawnPoint() {
            Rect worldBorders = GetWorldLimits(Config.screenSpawnOutsideOffset);

            Vector2 vector = new(Random.value, Random.value);
            Vector2 pos = new(worldBorders.x + worldBorders.width * vector.x, worldBorders.y + worldBorders.height * vector.y);

            Vector3 worldPoint;
            if (Random.value >= 0.5f)
                worldPoint = new Vector3(vector.x < 0.5f ? worldBorders.x : worldBorders.xMax, pos.y); // left/right
            else
                worldPoint = new Vector3(pos.x, vector.y < 0.5f ? worldBorders.y : worldBorders.yMax); // top/bottom

            return worldPoint;
        }

        private Vector3 GetRandomDirection(Vector3 spawnPoint) {
            float randomAngle = (Random.value - 0.5f) * 90f;
            Vector2 direction = -spawnPoint.normalized;
            direction = Quaternion.AngleAxis(randomAngle, Vector3.forward) * direction;

            return direction;
        }

        private void EnemyHitHandler(ICollider enemy, ICollider ammo) {
            if (ammo is Bullet bullet) bullet.Destroy();
            switch (enemy) {
                case Asteroid asteroid:
                    asteroid.Destroy();
                    break;
                case Ufo ufo:
                    ufo.Destroy();
                    break;
            }
        }

        private void AsteroidExplosionHandler(Asteroid destroyedAsteroid) {
            if (GameStatus.Value != Core.Game.GameStatus.Playing) return; // hack

            if (destroyedAsteroid.Size == AsteroidConfig.Size.Medium) return; // Don't split medium asteroids
            if (destroyedAsteroid.Size == AsteroidConfig.Size.Small) return;

            AsteroidConfig.Size targetSize = destroyedAsteroid.Size;
            if (destroyedAsteroid.Size == AsteroidConfig.Size.Large) targetSize = AsteroidConfig.Size.Medium;
            else if (destroyedAsteroid.Size == AsteroidConfig.Size.Medium) targetSize = AsteroidConfig.Size.Small;

            Vector3 direction = Random.insideUnitCircle;
            float degreesDelta = 360f / destroyedAsteroid.DestroyedFragments;
            for (int i = 0; i < destroyedAsteroid.DestroyedFragments; i++) {
                Asteroid newAsteroid = AsteroidPools[targetSize].Take();
                direction = Quaternion.AngleAxis(degreesDelta, Vector3.forward) * direction;
                Vector3 spawnPoint = destroyedAsteroid.Transform.position + direction * 0.5f;
                newAsteroid.Transform.position = spawnPoint;
                newAsteroid.Set(direction);
            }
        }

    }
}