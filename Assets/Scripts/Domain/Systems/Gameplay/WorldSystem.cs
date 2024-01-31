using Domain.Base;
using Domain.Systems.Collision;
using Framework.Base;
using Framework.Objects;
using Presentation.Data;
using Presentation.GUI;
using Presentation.Objects;
using Vector3 = UnityEngine.Vector3;

namespace Domain.Systems.Gameplay {
    public class WorldSystem : IUpdate {
        private readonly EntityPool<Asteroid, AsteroidData> asteroidsPool;
        private readonly EntityPool<Ufo, UfoData> ufosPool;

        private readonly WorldData worldData;

        private float nextAsteroidSpawn;
        private float nextUfoSpawn;

        public WorldSystem(DataCollector dataCollector, PrefabCollector prefabCollector) {
            // Pools
            asteroidsPool = new EntityPool<Asteroid, AsteroidData>(prefabCollector.asteroid, dataCollector.asteroidData);
            ufosPool = new EntityPool<Ufo, UfoData>(prefabCollector.ufo, dataCollector.ufoData);

            // World
            worldData = dataCollector.worldData;
            nextAsteroidSpawn = 1 / worldData.asteroidsSpawnRate;
            nextAsteroidSpawn = 1 / worldData.ufoSpawnRate;

            // Subscribe
            CollisionSystem.enemyHit += EnemyHitHandler;
        }

        public void Upd(float deltaTime) {
            if ((nextAsteroidSpawn -= deltaTime) <= 0) {
                nextAsteroidSpawn = 1 / worldData.asteroidsSpawnRate;
                SpawnAsteroid();
            }

            if ((nextUfoSpawn -= deltaTime) <= 0) {
                nextUfoSpawn = 1 / worldData.ufoSpawnRate;
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