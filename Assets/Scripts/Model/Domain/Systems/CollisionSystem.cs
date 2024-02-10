using System.Collections.Generic;
using Model.Core.Adapters;
using Model.Core.Data;
using Model.Core.Data.State;
using Model.Core.Entity;
using Model.Core.Interface.Entity;
using Model.Core.Pools;
using Model.Core.Unity.Data.Config;
using Model.Domain.Systems.Base;
using UnityEngine;

namespace Model.Domain.Systems {
    public class CollisionSystem : SystemBase, IUpdateSystem {
        private ICollider Player { get; }

        // Active entities in world
        private Dictionary<AsteroidConfig.Size, AsteroidPool> AsteroidPools { get; }
        private IEnumerable<Ufo> Ufos { get; }
        private IEnumerable<Bullet> Bullets { get; }
        private IEnumerable<Laser> Lasers { get; }

        // Events
        public delegate void PlayerHitEventHandler(ICollider enemy);
        public delegate void EnemyHitEventHandler(ICollider enemy, ICollider ammo);

        public static event PlayerHitEventHandler PlayerHitEvent;
        public static event EnemyHitEventHandler EnemyHitEvent;


        public CollisionSystem(DataCollector data, AdaptersCollector adapters) {
            EntitiesState entities = data.States.entity;

            // Link properties
            Player = (ICollider)entities.player;
            AsteroidPools = entities.asteroidPools;
            Ufos = entities.ufosPool.Active;
            Bullets = entities.ammo1Pool.Active;
            Lasers = entities.ammo2Pool.Active;

            // Game state
            GameStateSystem.NewGameEvent += Enable;
            GameStateSystem.GameOverEvent += Disable;
        }

        public void Upd(float deltaTime) {
            IEnumerator<Asteroid> asteroidsLargeEnum = AsteroidPools[AsteroidConfig.Size.Large].Active.GetEnumerator();
            IEnumerator<Asteroid> asteroidsMediumEnum = AsteroidPools[AsteroidConfig.Size.Medium].Active.GetEnumerator();
            IEnumerator<Asteroid> asteroidsSmallEnum = AsteroidPools[AsteroidConfig.Size.Small].Active.GetEnumerator();
            IEnumerator<Ufo> ufosEnum = Ufos.GetEnumerator();

            do {
                ICollider enemy;
                if (asteroidsLargeEnum.MoveNext()) enemy = asteroidsLargeEnum.Current;
                else if (asteroidsMediumEnum.MoveNext()) enemy = asteroidsMediumEnum.Current;
                else if (asteroidsSmallEnum.MoveNext()) enemy = asteroidsSmallEnum.Current;
                else if (ufosEnum.MoveNext()) enemy = ufosEnum.Current;
                else break;
                if (enemy == null) break;

                // Check bullets
                bool toBreak = false;
                foreach (ICollider ammo in Bullets) {
                    if (IsIntersect(enemy, ammo)) {
                        EnemyHitEvent?.Invoke(enemy, ammo);
                        toBreak = true;
                        break;
                    }
                }
                if (toBreak) break;

                // Check laser
                foreach (Laser laser in Lasers) {
                    float enemyDistance = Vector2.Distance(enemy.Pos, laser.Pos);
                    // Check laser distance limit
                    if (laser.MaxDistance + laser.ColliderRadius < enemyDistance - enemy.ColliderRadius) continue;
                    // Find nearest laser point to enemy
                    Vector2 laserNearestPoint = laser.Pos + laser.Direction * enemyDistance;

                    float distance = Vector2.Distance(enemy.Pos, laserNearestPoint);
                    float collisionDistance = laser.ColliderRadius + enemy.ColliderRadius;
                    if (distance <= collisionDistance) {
                        EnemyHitEvent?.Invoke(enemy, laser);
                        toBreak = true;
                        // Don't break here (for laser)
                    }
                }
                if (toBreak) break;

                // Check player (latest, after bullets)
                if (IsIntersect(enemy, Player)) {
                    PlayerHitEvent?.Invoke(enemy);
                    break;
                }

            } while (true);

            asteroidsLargeEnum.Dispose();
            asteroidsMediumEnum.Dispose();
            asteroidsSmallEnum.Dispose();
            ufosEnum.Dispose();
        }

        private bool IsIntersect(ICollider collider1, ICollider collider2) {
            return Vector2.Distance(collider1.Pos, collider2.Pos) < collider1.ColliderRadius + collider2.ColliderRadius;
        }
    }
}