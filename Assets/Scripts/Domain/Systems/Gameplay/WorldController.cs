using Domain.Base;
using Domain.Systems.Collision;
using Framework.Base;
using Framework.Objects;
using Presentation.Data;
using Presentation.GUI;
using Presentation.Objects;
using Vector3 = UnityEngine.Vector3;

namespace Domain.Systems.Gameplay {
    public class WorldController : IUpdate {
        private readonly EntityPool<Asteroid, AsteroidData> asteroidsPool;
        private readonly EntityPool<Ufo, UfoData> ufosPool;

        private readonly EnvironmentData environmentData;

        private float nextAsteroidSpawn;
        private float nextUfoSpawn;

        public WorldController(DataCollector dataCollector, PrefabCollector prefabCollector) {
            // Pools
            asteroidsPool = new EntityPool<Asteroid, AsteroidData>(prefabCollector.asteroid, dataCollector.asteroidData);
            ufosPool = new EntityPool<Ufo, UfoData>(prefabCollector.ufo, dataCollector.ufoData);

            // Environment
            environmentData = dataCollector.environmentData;
            nextAsteroidSpawn = 1 / environmentData.asteroidsSpawnRate;
            nextAsteroidSpawn = 1 / environmentData.ufoSpawnRate;

            // Subscribe
            CollisionSystem.enemyHit += EnemyHitHandler;
        }

        public void Upd(float deltaTime) {
            if ((nextAsteroidSpawn -= deltaTime) <= 0) {
                nextAsteroidSpawn = 1 / environmentData.asteroidsSpawnRate;
                SpawnAsteroid();
            }

            if ((nextUfoSpawn -= deltaTime) <= 0) {
                nextUfoSpawn = 1 / environmentData.ufoSpawnRate;
                SpawnUfo();
            }
        }

        private void SpawnAsteroid() {
            SpawnEntity(asteroidsPool.Take());
        }

        private void SpawnUfo() {
            SpawnEntity(ufosPool.Take());
        }

        private void SpawnEntity(EntityBase entity) {
            Vector3 spawnPoint = GetRandomSpawnPoint();
            entity.transform.localPosition = spawnPoint;
            entity.transform.forward = GetStartDirection(spawnPoint);
        }

        private Vector3 GetRandomSpawnPoint() {
            Vector3 spawnPoint = new Vector3(0, 0);
            // todo..
            return spawnPoint;
        }

        private Vector3 GetStartDirection(Vector3 spawnPoint) {
            Vector3 direction = new Vector3(0, 0);
            // todo..
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