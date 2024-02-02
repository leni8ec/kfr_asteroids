using System.Collections.Generic;
using Core.Base;
using Core.Config;
using Core.Interface.Base;
using Core.Interface.Objects;
using Core.Objects;
using UnityEngine;

namespace Domain.Systems.Collision {
    public class CollisionSystem : IUpdate {
        private readonly Player player;
        private readonly Dictionary<Asteroid.Size, EntityPool<Asteroid, AsteroidConfig>> asteroidPools;
        private readonly List<Ufo> ufos;
        private readonly List<Bullet> bullets;
        private readonly List<Laser> lasers;

        public delegate void PlayerHitEvent(ICollider enemy);
        public delegate void EnemyHitEvent(ICollider enemy, ICollider ammo);

        public static event PlayerHitEvent PlayerHit;
        public static event EnemyHitEvent EnemyHit;

        public CollisionSystem(Player player, Dictionary<Asteroid.Size, EntityPool<Asteroid, AsteroidConfig>> asteroidPools, List<Ufo> ufos, List<Bullet> bullets, List<Laser> lasers) {
            this.player = player;
            this.asteroidPools = asteroidPools;
            this.ufos = ufos;
            this.bullets = bullets;
            this.lasers = lasers;
        }

        public void Upd(float deltaTime) {

            List<Asteroid>.Enumerator asteroidsLargeEnum = asteroidPools[Asteroid.Size.Large].active.GetEnumerator();
            List<Asteroid>.Enumerator asteroidsMediumEnum = asteroidPools[Asteroid.Size.Medium].active.GetEnumerator();
            List<Asteroid>.Enumerator asteroidsSmallEnum = asteroidPools[Asteroid.Size.Small].active.GetEnumerator();
            List<Ufo>.Enumerator ufosEnum = ufos.GetEnumerator();

            ICollider enemy;
            do {
                if (asteroidsLargeEnum.MoveNext()) enemy = asteroidsLargeEnum.Current;
                else if (asteroidsMediumEnum.MoveNext()) enemy = asteroidsMediumEnum.Current;
                else if (asteroidsSmallEnum.MoveNext()) enemy = asteroidsSmallEnum.Current;
                else if (ufosEnum.MoveNext()) enemy = ufosEnum.Current;
                else break;
                if (enemy == null) break;

                // Check bullets
                bool toBreak = false;
                foreach (ICollider ammo in bullets) {
                    if (intersect(enemy, ammo)) {
                        EnemyHit?.Invoke(enemy, ammo);
                        toBreak = true;
                        break;
                    }
                }
                if (toBreak) break;

                // Check laser
                foreach (Laser laser in lasers) {
                    float enemyDistance = Vector2.Distance(enemy.Pos, laser.Pos);
                    // Check laser distance limit
                    if (laser.MaxDistance + laser.Radius < enemyDistance - enemy.Radius) continue;
                    // Find nearest laser point to enemy
                    Vector2 laserNearestPoint = laser.Pos + laser.Direction * enemyDistance;

                    float distance = Vector2.Distance(enemy.Pos, laserNearestPoint);
                    float collisionDistance = laser.Radius + enemy.Radius;
                    if (distance <= collisionDistance) {
                        EnemyHit?.Invoke(enemy, laser);
                        toBreak = true;
                        // Don't break here (for laser)
                    }
                }
                if (toBreak) break;

                // Check player (latest, after bullets)
                if (intersect(enemy, player)) {
                    PlayerHit?.Invoke(enemy);
                    break;
                }

            } while (true);
            asteroidsLargeEnum.Dispose();
            asteroidsMediumEnum.Dispose();
            asteroidsSmallEnum.Dispose();
            ufosEnum.Dispose();
        }

        private bool intersect(ICollider collider1, ICollider collider2) {
            return Vector2.Distance(collider1.Pos, collider2.Pos) < collider1.Radius + collider2.Radius;
        }
    }
}