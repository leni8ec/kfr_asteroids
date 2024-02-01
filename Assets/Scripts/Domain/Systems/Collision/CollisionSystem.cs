using System.Collections.Generic;
using Domain.Base;
using Framework.Base;
using Framework.Objects;
using Presentation.Data;
using Presentation.Objects;
using UnityEngine;

namespace Domain.Systems.Collision {
    public class CollisionSystem : IUpdate {
        private readonly ICollider player;
        private readonly Dictionary<Asteroid.Size, EntityPool<Asteroid, AsteroidData>> asteroidPools;
        private readonly List<Ufo> ufos;
        private readonly List<Bullet> bullets;

        public delegate void PlayerHitEvent(ICollider enemy);
        public delegate void EnemyHitEvent(ICollider enemy, ICollider ammo);

        public static event PlayerHitEvent PlayerHit;
        public static event EnemyHitEvent EnemyHit;

        public CollisionSystem(ICollider player, Dictionary<Asteroid.Size, EntityPool<Asteroid, AsteroidData>> asteroidPools, List<Ufo> ufos, List<Bullet> bullets) {
            this.player = player;
            this.asteroidPools = asteroidPools;
            this.ufos = ufos;
            this.bullets = bullets;
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

                // Check player (latest, after bullets)
                if (intersect(enemy, player)) {
                    PlayerHit?.Invoke(enemy);
                    break;
                }

            } while (enemy != null);
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