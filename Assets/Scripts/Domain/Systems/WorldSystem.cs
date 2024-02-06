using System.Collections.Generic;
using Core.Config;
using Core.Interface.Objects;
using Core.Objects;
using Core.Pools;
using Core.State;
using Core.Unity;
using Domain.Base;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Domain.Systems {
    public class WorldSystem : SystemBase, IUpdateSystem {
        private WorldState State { get; }
        private WorldConfig Config { get; }

        private Player Player { get; }
        private Camera Camera { get; }

        private List<Bullet> ActiveBullets { get; }

        private UfoPool UfoPool { get; }
        private Dictionary<AsteroidConfig.Size, AsteroidPool> AsteroidPools { get; }


        private bool active;

        public WorldSystem(StateCollector state, ConfigCollector configCollector, PrefabCollector prefabCollector) {
            State = state.world;
            Config = configCollector.world;

            // Fill objects state
            ObjectsState objects = state.objects;
            objects.ufosPool = new UfoPool(prefabCollector.ufo, configCollector.ufo);
            objects.asteroidPools = new Dictionary<AsteroidConfig.Size, AsteroidPool> {
                { AsteroidConfig.Size.Large, new AsteroidPool(prefabCollector.asteroidLarge, configCollector.asteroidLarge) },
                { AsteroidConfig.Size.Medium, new AsteroidPool(prefabCollector.asteroidMedium, configCollector.asteroidMedium) },
                { AsteroidConfig.Size.Small, new AsteroidPool(prefabCollector.asteroidSmall, configCollector.asteroidSmall) }
            };

            // Link properties
            Player = objects.player;
            Camera = objects.camera;
            ActiveBullets = objects.ammo1Pool.active;
            UfoPool = objects.ufosPool;
            AsteroidPools = objects.asteroidPools;

            // Subscribe
            CollisionSystem.EnemyHit += EnemyHitHandler;
            Asteroid.Explosion += AsteroidExplosionHandler;

            // Game state
            GameStateSystem.NewGameEvent += PlayHandler;
            GameStateSystem.GameOverEvent += ResetHandler;
        }

        private void PlayHandler() {
            active = true;

            State.asteroidSpawnCountdown = 1 / Config.asteroidsSpawnRate;
            State.ufoSpawnCountdown = 1 / Config.ufoSpawnRate;
        }

        private void ResetHandler() {
            Reset();
            active = false;
            for (int i = UfoPool.active.Count - 1; i >= 0; i--) UfoPool.active[i].Destroy();
            foreach (AsteroidPool asteroidsPool in AsteroidPools.Values) {
                for (int i = asteroidsPool.active.Count - 1; i >= 0; i--) asteroidsPool.active[i].Destroy();
            }
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void Reset() {
            State.Reset();
        }

        public void Upd(float deltaTime) {
            if (!active) return;
            // Spawn
            if ((State.asteroidSpawnCountdown -= deltaTime) <= 0) {
                // Check asteroids count limit
                int totalActiveAsteroids = AsteroidPools[AsteroidConfig.Size.Large].active.Count
                                           + AsteroidPools[AsteroidConfig.Size.Medium].active.Count
                                           + AsteroidPools[AsteroidConfig.Size.Small].active.Count;
                if (totalActiveAsteroids < Config.asteroidsLimit) {
                    State.asteroidSpawnCountdown = 1 / Config.asteroidsSpawnRate;
                    SpawnAsteroid();
                }
            }

            if ((State.ufoSpawnCountdown -= deltaTime) <= 0) {
                int totalActiveUfo = UfoPool.active.Count;
                if (totalActiveUfo < Config.ufosLimit) {
                    State.ufoSpawnCountdown = 1 / Config.ufoSpawnRate;
                    SpawnUfo();
                }
            }

            ProcessInfinityScreen();
            // CheckDisposeOutOfScreenObjects(deltaTime);

        }

        private Rect GetWorldLimits(float screenOffset) {
            Vector2 min = Camera.ScreenToWorldPoint(Vector3.zero);
            Vector2 max = Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            Rect limits = new(min.x - screenOffset, min.y - screenOffset, max.x - min.x + screenOffset * 2, max.y - min.y + screenOffset * 2);
            return limits;
        }

        public List<Asteroid> GetActiveAsteroids(AsteroidConfig.Size size) {
            return AsteroidPools[size].active;
        }

        private void ProcessInfinityScreen() {
            Rect worldBorders = GetWorldLimits(Config.screenInfinityOutsideOffset);

            // Player
            ProcessObjectOutOfScreen(worldBorders, Player.Transform);
            // Enemies
            foreach (Ufo ufo in UfoPool.active) ProcessObjectOutOfScreen(worldBorders, ufo.Transform);
            foreach (AsteroidPool asteroidsPool in AsteroidPools.Values) {
                for (int i = asteroidsPool.active.Count - 1; i >= 0; i--) {
                    Asteroid asteroid = asteroidsPool.active[i];
                    ProcessObjectOutOfScreen(worldBorders, asteroid.Transform);
                }
            }
            // Bullets
            foreach (Bullet bullet in ActiveBullets) ProcessObjectOutOfScreen(worldBorders, bullet.Transform);
        }

        private void ProcessObjectOutOfScreen(Rect worldBorders, Transform target) {
            Vector3 pos = target.position;
            if (worldBorders.Contains(pos)) return;

            if (pos.x < worldBorders.x) pos.x = worldBorders.xMax;
            else if (pos.y < worldBorders.y) pos.y = worldBorders.yMax;
            else if (pos.x > worldBorders.xMax) pos.x = worldBorders.x;
            else if (pos.y > worldBorders.yMax) pos.y = worldBorders.y;

            target.position = pos;
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