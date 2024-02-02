using System.Collections.Generic;
using Core.Base;
using Core.Config;
using Core.Interface.Base;
using Core.Interface.Objects;
using Core.Objects;
using Core.State;
using Core.Unity;
using Domain.Systems.Collision;
using Domain.Systems.Game;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Domain.Systems.Gameplay {
    public class WorldSystem : IUpdate {
        private WorldState State { get; }
        private Player Player { get; }
        private List<Bullet> ActiveBullets { get; }
        private Camera MainCamera { get; }
        private EntityPool<Ufo, UfoConfig> UfosPool { get; }
        public Dictionary<Asteroid.Size, EntityPool<Asteroid, AsteroidConfig>> AsteroidPools { get; }

        public List<Ufo> ActiveUfos => UfosPool.active;

        private WorldConfig Config { get; }

        private bool active;

        public WorldSystem(WorldState state, Player player, List<Bullet> activeBullets, ConfigCollector configCollector, PrefabCollector prefabCollector, Camera mainCamera) {
            State = state;

            Player = player;
            ActiveBullets = activeBullets;
            MainCamera = mainCamera;
            // Pools
            UfosPool = new EntityPool<Ufo, UfoConfig>(prefabCollector.ufo, configCollector.ufo);
            AsteroidPools = new Dictionary<Asteroid.Size, EntityPool<Asteroid, AsteroidConfig>> {
                { Asteroid.Size.Large, new EntityPool<Asteroid, AsteroidConfig>(prefabCollector.asteroidLarge, configCollector.asteroidLarge) },
                { Asteroid.Size.Medium, new EntityPool<Asteroid, AsteroidConfig>(prefabCollector.asteroidMedium, configCollector.asteroidMedium) },
                { Asteroid.Size.Small, new EntityPool<Asteroid, AsteroidConfig>(prefabCollector.asteroidSmall, configCollector.asteroidSmall) }
            };

            // World
            Config = configCollector.world;
            State.asteroidSpawnCountdown = 1 / Config.asteroidsSpawnRate;
            State.ufoSpawnCountdown = 1 / Config.ufoSpawnRate;

            // Subscribe
            CollisionSystem.EnemyHit += EnemyHitHandler;
            Asteroid.Explosion += OnAsteroidExplosion;

            // Game state
            GameStateSystem.NewGameEvent += Play;
            GameStateSystem.GameOverEvent += Reset;
        }

        private void Play() {
            active = true;
        }

        private void Reset() {
            active = false;
            for (int i = UfosPool.active.Count - 1; i >= 0; i--) UfosPool.active[i].Reset();
            foreach (EntityPool<Asteroid, AsteroidConfig> asteroidsPool in AsteroidPools.Values) {
                for (int i = asteroidsPool.active.Count - 1; i >= 0; i--) asteroidsPool.active[i].Reset();
            }
        }

        public void Upd(float deltaTime) {
            if (!active) return;
            // Spawn
            if ((State.asteroidSpawnCountdown -= deltaTime) <= 0) {
                // Check asteroids count limit
                int totalActiveAsteroids = AsteroidPools[Asteroid.Size.Large].active.Count + AsteroidPools[Asteroid.Size.Medium].active.Count + AsteroidPools[Asteroid.Size.Small].active.Count;
                if (totalActiveAsteroids < Config.asteroidsLimit) {
                    State.asteroidSpawnCountdown = 1 / Config.asteroidsSpawnRate;
                    SpawnAsteroid();
                }
            }

            if ((State.ufoSpawnCountdown -= deltaTime) <= 0) {
                int totalActiveUfo = UfosPool.active.Count;
                if (totalActiveUfo < Config.ufosLimit) {
                    State.ufoSpawnCountdown = 1 / Config.ufoSpawnRate;
                    SpawnUfo();
                }
            }

            ProcessInfinityScreen();
            // CheckDisposeOutOfScreenObjects(deltaTime);

        }

        private Rect GetWorldLimits(float screenOffset) {
            Vector2 min = MainCamera.ScreenToWorldPoint(Vector3.zero);
            Vector2 max = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            Rect limits = new(min.x - screenOffset, min.y - screenOffset, max.x - min.x + screenOffset * 2, max.y - min.y + screenOffset * 2);
            return limits;
        }

        public List<Asteroid> GetActiveAsteroids(Asteroid.Size size) {
            return AsteroidPools[size].active;
        }

        private void ProcessInfinityScreen() {
            Rect worldBorders = GetWorldLimits(Config.screenInfinityOutsideOffset);

            // Player
            ProcessObjectOutOfScreen(worldBorders, Player.transform);
            // Enemies
            foreach (Ufo ufo in UfosPool.active) ProcessObjectOutOfScreen(worldBorders, ufo.transform);
            foreach (EntityPool<Asteroid, AsteroidConfig> asteroidsPool in AsteroidPools.Values) {
                for (int i = asteroidsPool.active.Count - 1; i >= 0; i--) {
                    Asteroid asteroid = asteroidsPool.active[i];
                    ProcessObjectOutOfScreen(worldBorders, asteroid.transform);
                }
            }
            // Bullets
            foreach (Bullet bullet in ActiveBullets) ProcessObjectOutOfScreen(worldBorders, bullet.transform);
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

        private void CheckDisposeOutOfScreenObjects(float deltaTime) {
            // Dispose checking
            if ((State.disposeCountdown -= deltaTime) <= 0) {
                State.disposeCountdown = State.disposeInterval;
                // Asteroids
                foreach (EntityPool<Asteroid, AsteroidConfig> asteroidsPool in AsteroidPools.Values) {
                    for (int i = asteroidsPool.active.Count - 1; i >= 0; i--) {
                        Asteroid asteroid = asteroidsPool.active[i];
                        if (asteroid.Lifetime < 10) continue;

                        // Check if asteroid is outside world
                        Vector3 point = MainCamera.WorldToViewportPoint(asteroid.transform.position);
                        Vector2 borders = Config.viewportOutsideBorders;
                        if (point.x < borders.x || point.x > borders.y || point.y < borders.x || point.y > borders.y) {
                            asteroid.Reset();
                        }
                    }
                }
            }
        }


        private void SpawnAsteroid() {
            Asteroid asteroid = AsteroidPools[Asteroid.Size.Large].Take();
            Vector3 spawnPoint = GetRandomSpawnPoint();
            Vector3 direction = GetRandomDirection(spawnPoint);
            asteroid.transform.position = spawnPoint;
            asteroid.Set(direction);
        }

        private void SpawnUfo() {
            Ufo ufo = UfosPool.Take();
            Vector3 spawnPoint = GetRandomSpawnPoint();
            Vector3 direction = GetRandomDirection(spawnPoint);
            ufo.transform.position = spawnPoint;
            ufo.Set(direction);
            ufo.SetTarget(Player.transform);
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

        private void OnAsteroidExplosion(Asteroid destroyedAsteroid) {
            if (destroyedAsteroid.size == Asteroid.Size.Medium) return; // Don't split medium asteroids
            if (destroyedAsteroid.size == Asteroid.Size.Small) return;

            Asteroid.Size targetSize = destroyedAsteroid.size;
            if (destroyedAsteroid.size == Asteroid.Size.Large) targetSize = Asteroid.Size.Medium;
            else if (destroyedAsteroid.size == Asteroid.Size.Medium) targetSize = Asteroid.Size.Small;

            Vector3 direction = Random.insideUnitCircle;
            float degreesDelta = 360f / State.asteroidDestroyFragments;
            for (int i = 0; i < State.asteroidDestroyFragments; i++) {
                Asteroid newAsteroid = AsteroidPools[targetSize].Take();
                direction = Quaternion.AngleAxis(degreesDelta, Vector3.forward) * direction;
                Vector3 spawnPoint = destroyedAsteroid.transform.position + direction * 0.5f;
                newAsteroid.transform.position = spawnPoint;
                newAsteroid.Set(direction);
            }
        }

    }
}