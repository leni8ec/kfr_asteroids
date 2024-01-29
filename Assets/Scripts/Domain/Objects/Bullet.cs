using Domain.Data;
using Framework.Objects;
using UnityEngine;

namespace Domain.Objects {
    public class Bullet : Entity<BulletData>, IBullet {
        public float startPoint;
        public Vector2 direction;

        public Bullet(BulletData data) : base(data) { }

        public void Upd(float deltaTime) { }

    }
}