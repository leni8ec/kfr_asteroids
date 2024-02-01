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
        private readonly EntityPool<Asteroid, AsteroidData> asteroidsPool;
        private readonly EntityPool<Ufo, UfoData> ufosPool;

        private readonly WorldData worldData;

        private float asteroidSpawnCountdown;
        private float ufoSpawnCountdown;

        public WorldSystem(DataCollector dataCollector, PrefabCollector prefabCollector) {
            // Pools
            asteroidsPool = new EntityPool<Asteroid, AsteroidData>(prefabCollector.asteroid, dataCollector.asteroidData);
            ufosPool = new EntityPool<Ufo, UfoData>(prefabCollector.ufo, dataCollector.ufoData);

            // World
            worldData = dataCollector.worldData;
            asteroidSpawnCountdown = 1 / worldData.asteroidsSpawnRate;
            ufoSpawnCountdown = 1 / worldData.ufoSpawnRate;

            // Subscribe
            CollisionSystem.enemyHit += EnemyHitHandler;
        }

        public void Upd(float deltaTime) {
            if ((asteroidSpawnCountdown -= deltaTime) <= 0) {
                asteroidSpawnCountdown = 1 / worldData.asteroidsSpawnRate;
                SpawnAsteroid();
            }

            if ((ufoSpawnCountdown -= deltaTime) <= 0) {
                ufoSpawnCountdown = 1 / worldData.ufoSpawnRate;
                SpawnUfo();
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
        }


        private Vector3 GetRandomSpawnPoint() {
            Vector2 borders = new(-0.1f, 1.1f);
            Vector2 vector = new(Random.value, Random.value);
            Vector2 viewportBorderPoint;
            if (Random.value >= 0.5f)
                viewportBorderPoint = new Vector2(vector.x < 0.5f ? borders.x : borders.y, vector.y); // left/right
            else
                viewportBorderPoint = new Vector2(vector.x, vector.y < 0.5f ? borders.x : borders.y); // top/bottom

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
            if (enemy is Asteroid asteroid) {
                asteroidsPool.Return(asteroid);
            } else if (enemy is Ufo ufo) {
                ufosPool.Return(ufo);
            }
        }

    }
}