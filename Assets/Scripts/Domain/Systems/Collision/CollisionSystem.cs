using System.Collections.Generic;
using Framework.Base;
using Framework.Objects;
using Presentation.Objects;
using UnityEngine;

namespace Domain.Systems.Collision {
    public class CollisionSystem : IUpdate {
        private ICollider player;
        private readonly List<Asteroid> asteroids;
        private readonly List<Ufo> ufos;
        private readonly List<Bullet> bullets;

        public delegate void PlayerHitEvent(ICollider enemy);
        public delegate void EnemyHitEvent(ICollider enemy, ICollider ammo);

        public static event PlayerHitEvent PlayerHit;
        public static event EnemyHitEvent EnemyHit;

        public CollisionSystem(ICollider player, List<Asteroid> asteroids, List<Ufo> ufos, List<Bullet> bullets) {
            this.player = player;
            this.asteroids = asteroids;
            this.ufos = ufos;
            this.bullets = bullets;
        }

        public void Upd(float deltaTime) {

            List<Asteroid>.Enumerator asteroidsEnum = asteroids.GetEnumerator();
            List<Ufo>.Enumerator ufosEnum = ufos.GetEnumerator();

            ICollider enemy;
            do {
                if (asteroidsEnum.MoveNext()) enemy = asteroidsEnum.Current;
                else if (ufosEnum.MoveNext()) enemy = ufosEnum.Current;
                else break;

                // Check player
                if (intersect(enemy, player)) {
                    PlayerHit(enemy);
                    break;
                }

                // Check bullets
                bool toBreak = false;
                foreach (ICollider ammo in bullets) {
                    if (intersect(enemy, ammo)) {
                        EnemyHit(enemy, ammo);
                        toBreak = true;
                        break;
                    }
                }
                if (toBreak) break;

            } while (enemy != null);
            asteroidsEnum.Dispose();
            ufosEnum.Dispose();
        }

        private bool intersect(ICollider collider1, ICollider collider2) {
            return Vector2.Distance(collider1.Pos, collider2.Pos) < collider1.Radius + collider2.Radius;
        }
    }
}