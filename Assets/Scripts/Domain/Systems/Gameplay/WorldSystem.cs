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
        private readonly EntityPool<Asteroid, AsteroidData> asteroidsPool;
        private readonly EntityPool<Ufo, UfoData> ufosPool;

        public List<Asteroid> ActiveAsteroids => asteroidsPool.active;
        public List<Ufo> ActiveUfos => ufosPool.active;

        private readonly WorldData worldData;

        private float asteroidSpawnCountdown;
        private float ufoSpawnCountdown;

        private readonly float disposeInterval = 1;
        private float disposeCountdown = 10;

        public WorldSystem(Player player, DataCollector dataCollector, PrefabCollector prefabCollector) {
            this.player = player;
            // Pools
            asteroidsPool = new EntityPool<Asteroid, AsteroidData>(prefabCollector.asteroidLarge, dataCollector.asteroidLargeData);
            ufosPool = new EntityPool<Ufo, UfoData>(prefabCollector.ufo, dataCollector.ufoData);

            // World
            worldData = dataCollector.worldData;
            asteroidSpawnCountdown = 1 / worldData.asteroidsSpawnRate;
            ufoSpawnCountdown = 1 / worldData.ufoSpawnRate;

            // Subscribe
            CollisionSystem.enemyHit += EnemyHitHandler;
        }

        public void Upd(float deltaTime) {
            // Spawn
            if ((asteroidSpawnCountdown -= deltaTime) <= 0) {
                asteroidSpawnCountdown = 1 / worldData.asteroidsSpawnRate;
                SpawnAsteroid();
            }

            if ((ufoSpawnCountdown -= deltaTime) <= 0) {
                ufoSpawnCountdown = 1 / worldData.ufoSpawnRate;
                SpawnUfo();
            }

            // Dispose checking
            if ((disposeCountdown -= deltaTime) <= 0) {
                disposeCountdown = disposeInterval;

                for (int index = asteroidsPool.active.Count - 1; index >= 0; index--) {
                    Asteroid asteroid = asteroidsPool.active[index];
                    if (asteroid.Lifetime < 10) continue;

                    // Check if asteroid is outside world
                    Vector3 point = GuiController.Handler.mainCamera.WorldToViewportPoint(asteroid.transform.position);
                    Vector2 borders = worldData.viewportOutsideBorders;
                    if (point.x < borders.x || point.x > borders.y || point.y < borders.x || point.y > borders.y) {
                        asteroid.Reset();
                    }
                }

                foreach (Ufo ufo in ufosPool.active) { }
            }


        }

        private void SpawnAsteroid() {
            Asteroid asteroid = asteroidsPool.Take();
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
            Vector2 vector = new(Random.value, Random.value);
            Vector2 viewportBorderPoint;
            if (Random.value >= 0.5f)
                viewportBorderPoint = new Vector2(vector.x < 0.5f ? worldData.viewportOutsideBorders.x : worldData.viewportOutsideBorders.y, vector.y); // left/right
            else
                viewportBorderPoint = new Vector2(vector.x, vector.y < 0.5f ? worldData.viewportOutsideBorders.x : worldData.viewportOutsideBorders.y); // top/bottom

            Vector3 worldPoint = GuiController.Handler.mainCamera.ViewportToWorldPoint(viewportBorderPoint);
            worldPoint.z = 0;
            return worldPoint;
        }

        private Vector3 GetRandomDirection(Vector3 spawnPoint) {
            float randomAngle = (Random.value - 0.5f) * 90f;
            Vector2 direction = -spawnPoint.normalized;
            direction = Quaternion.AngleAxis(randomAngle, Vector3.forward) * direction;

            return direction;
        }

        private void EnemyHitHandler(ICollider enemy, ICollider ammo) {
            Debug.Log("EnemyHitHandler");
            if (ammo is Bullet bullet) bullet.Reset();
            switch (enemy) {
                case Asteroid asteroid:
                    asteroid.Reset();
                    break;
                case Ufo ufo:
                    ufo.Reset();
                    break;
            }
        }

    }
}