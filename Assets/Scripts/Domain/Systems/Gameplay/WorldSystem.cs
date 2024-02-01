using System.Collections.Generic;
using Domain.Base;
using Domain.Systems.Collision;
using Framework.Base;
using Framework.Objects;
using Presentation.Data;
using Presentation.GUI;
using Presentation.Objects;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Domain.Systems.Gameplay {
    public class WorldSystem : IUpdate {
        private readonly Player player;
        private readonly List<Bullet> activeBullets;
        private readonly EntityPool<Ufo, UfoData> ufosPool;
        private readonly Dictionary<Asteroid.Size, EntityPool<Asteroid, AsteroidData>> asteroidPools;

        public List<Ufo> ActiveUfos => ufosPool.active;
        public Dictionary<Asteroid.Size, EntityPool<Asteroid, AsteroidData>> AsteroidPools => asteroidPools;

        private readonly WorldData data;


        private float asteroidSpawnCountdown;
        private float ufoSpawnCountdown;

        private const float DisposeInterval = 1;
        private float disposeCountdown = 10;

        private const int AsteroidDestroyFragments = 4;

        public WorldSystem(Player player, List<Bullet> activeBullets, DataCollector dataCollector, PrefabCollector prefabCollector) {
            this.player = player;
            this.activeBullets = activeBullets;
            // Pools
            ufosPool = new EntityPool<Ufo, UfoData>(prefabCollector.ufo, dataCollector.ufoData);
            asteroidPools = new Dictionary<Asteroid.Size, EntityPool<Asteroid, AsteroidData>> {
                { Asteroid.Size.Large, new EntityPool<Asteroid, AsteroidData>(prefabCollector.asteroidLarge, dataCollector.asteroidLargeData) },
                { Asteroid.Size.Medium, new EntityPool<Asteroid, AsteroidData>(prefabCollector.asteroidMedium, dataCollector.asteroidMediumData) },
                { Asteroid.Size.Small, new EntityPool<Asteroid, AsteroidData>(prefabCollector.asteroidSmall, dataCollector.asteroidSmallData) }
            };

            // World
            data = dataCollector.worldData;
            asteroidSpawnCountdown = 1 / data.asteroidsSpawnRate;
            ufoSpawnCountdown = 1 / data.ufoSpawnRate;

            // Subscribe
            CollisionSystem.EnemyHit += EnemyHitHandler;
            Asteroid.Explosion += OnAsteroidExplosion;
        }

        public void Upd(float deltaTime) {
            // Spawn
            if ((asteroidSpawnCountdown -= deltaTime) <= 0) {
                // Check asteroids count limit
                int totalActiveAsteroids = asteroidPools[Asteroid.Size.Large].active.Count + asteroidPools[Asteroid.Size.Medium].active.Count + asteroidPools[Asteroid.Size.Small].active.Count;
                if (totalActiveAsteroids < data.asteroidsLimit) {
                    asteroidSpawnCountdown = 1 / data.asteroidsSpawnRate;
                    SpawnAsteroid();
                }
            }

            if ((ufoSpawnCountdown -= deltaTime) <= 0) {
                int totalActiveUfo = ufosPool.active.Count;
                if (totalActiveUfo < data.ufosLimit) {
                    ufoSpawnCountdown = 1 / data.ufoSpawnRate;
                    SpawnUfo();
                }
            }

            ProcessInfinityScreen();
            // CheckDisposeOutOfScreenObjects(deltaTime);

        }

        private Rect GetWorldLimits(float screenOffset) {
            Vector2 min = GuiController.Handler.mainCamera.ScreenToWorldPoint(Vector3.zero);
            Vector2 max = GuiController.Handler.mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            Rect limits = new(min.x - screenOffset, min.y - screenOffset, max.x - min.x + screenOffset * 2, max.y - min.y + screenOffset * 2);
            return limits;
        }

        public List<Asteroid> GetActiveAsteroids(Asteroid.Size size) {
            return asteroidPools[size].active;
        }

        private void ProcessInfinityScreen() {
            Rect worldBorders = GetWorldLimits(data.screenInfinityOutsideOffset);

            // Player
            ProcessObjectOutOfScreen(worldBorders, player.transform);
            // Enemies
            foreach (Ufo ufo in ufosPool.active) ProcessObjectOutOfScreen(worldBorders, ufo.transform);
            foreach (EntityPool<Asteroid, AsteroidData> asteroidsPool in asteroidPools.Values) {
                for (int i = asteroidsPool.active.Count - 1; i >= 0; i--) {
                    Asteroid asteroid = asteroidsPool.active[i];
                    ProcessObjectOutOfScreen(worldBorders, asteroid.transform);
                }
            }
            // Bullets
            foreach (Bullet bullet in activeBullets) ProcessObjectOutOfScreen(worldBorders, bullet.transform);
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
            if ((disposeCountdown -= deltaTime) <= 0) {
                disposeCountdown = DisposeInterval;
                // Asteroids
                foreach (EntityPool<Asteroid, AsteroidData> asteroidsPool in asteroidPools.Values) {
                    for (int i = asteroidsPool.active.Count - 1; i >= 0; i--) {
                        Asteroid asteroid = asteroidsPool.active[i];
                        if (asteroid.Lifetime < 10) continue;

                        // Check if asteroid is outside world
                        Vector3 point = GuiController.Handler.mainCamera.WorldToViewportPoint(asteroid.transform.position);
                        Vector2 borders = data.viewportOutsideBorders;
                        if (point.x < borders.x || point.x > borders.y || point.y < borders.x || point.y > borders.y) {
                            asteroid.Reset();
                        }
                    }
                }
            }
        }


        private void SpawnAsteroid() {
            Asteroid asteroid = asteroidPools[Asteroid.Size.Large].Take();
            Vector3 spawnPoint = GetRandomSpawnPoint();
            Vector3 direction = GetRandomDirection(spawnPoint);
            asteroid.transform.position = spawnPoint;
            asteroid.Set(direction);
        }

        private void SpawnUfo() {
            Ufo ufo = ufosPool.Take();
            Vector3 spawnPoint = GetRandomSpawnPoint();
            Vector3 direction = GetRandomDirection(spawnPoint);
            ufo.transform.position = spawnPoint;
            ufo.Set(direction);
            ufo.SetTarget(player.transform);
        }


        private Vector3 GetRandomSpawnPoint() {
            Rect worldBorders = GetWorldLimits(data.screenSpawnOutsideOffset);

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
            const float degreesDelta = 360f / AsteroidDestroyFragments;
            for (int i = 0; i < AsteroidDestroyFragments; i++) {
                Asteroid newAsteroid = asteroidPools[targetSize].Take();
                direction = Quaternion.AngleAxis(degreesDelta, Vector3.forward) * direction;
                Vector3 spawnPoint = destroyedAsteroid.transform.position + direction * 0.5f;
                newAsteroid.transform.position = spawnPoint;
                newAsteroid.Set(direction);
            }
        }

    }
}