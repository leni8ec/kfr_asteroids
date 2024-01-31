using System.Collections.Generic;
using Framework.Base;
using Framework.Objects;
using UnityEngine;

namespace Domain.Systems.Collision {
    public class CollisionSystem : IUpdate {
        private ICollider player;
        private readonly List<ICollider> activeEnemies = new();
        private readonly List<ICollider> activeAmmo = new();

        public delegate void PlayerHit(ICollider enemy);
        public delegate void EnemyHit(ICollider enemy, ICollider ammo);

        public static PlayerHit playerHit;
        public static EnemyHit enemyHit;

        public CollisionSystem(ICollider player) {
            this.player = player;
        }

        public void Upd(float deltaTime) {

            foreach (ICollider enemy in activeEnemies) {
                if (intersect(enemy, player)) playerHit(enemy);

                foreach (ICollider ammo in activeAmmo) {
                    if (intersect(enemy, ammo)) enemyHit(enemy, ammo);
                }
            }
        }

        private bool intersect(ICollider collider1, ICollider collider2) {
            return Vector2.Distance(collider1.Pos, collider2.Pos) < collider1.Radius + collider2.Radius;
        }
    }
}