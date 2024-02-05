using System.Collections.Generic;
using Core.Config;
using Core.Interface.Objects;
using Core.Objects;
using Core.Pools;
using Core.State;
using Core.Unity;
using Domain.Base;
using UnityEngine;

namespace Domain.Systems {
    public class CollisionSystem : SystemBase, IUpdateSystem {
        private Player Player { get; }

        // Active objects in world
        private Dictionary<AsteroidConfig.Size, AsteroidPool> AsteroidPools { get; }
        private List<Ufo> Ufos { get; }
        private List<Bullet> Bullets { get; }
        private List<Laser> Lasers { get; }

        // Events
        public delegate void PlayerHitEvent(ICollider enemy);
        public delegate void EnemyHitEvent(ICollider enemy, ICollider ammo);

        public static event PlayerHitEvent PlayerHit;
        public static event EnemyHitEvent EnemyHit;


        public CollisionSystem(StateCollector state, ConfigCollector config, PrefabCollector prefab) {
            ObjectsState objects = state.objects;

            // Link properties
            Player = objects.player;
            AsteroidPools = objects.asteroidPools;
            Ufos = objects.ufosPool.active;
            Bullets = objects.ammo1Pool.active;
            Lasers = objects.ammo2Pool.active;
        }

        public void Upd(float deltaTime) {
            List<Asteroid>.Enumerator asteroidsLargeEnum = AsteroidPools[AsteroidConfig.Size.Large].active.GetEnumerator();
            List<Asteroid>.Enumerator asteroidsMediumEnum = AsteroidPools[AsteroidConfig.Size.Medium].active.GetEnumerator();
            List<Asteroid>.Enumerator asteroidsSmallEnum = AsteroidPools[AsteroidConfig.Size.Small].active.GetEnumerator();
            List<Ufo>.Enumerator ufosEnum = Ufos.GetEnumerator();

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
                        EnemyHit?.Invoke(enemy, ammo);
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
                        EnemyHit?.Invoke(enemy, laser);
                        toBreak = true;
                        // Don't break here (for laser)
                    }
                }
                if (toBreak) break;

                // Check player (latest, after bullets)
                if (IsIntersect(enemy, Player)) {
                    PlayerHit?.Invoke(enemy);
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