using Core.Config;
using Core.Interface.Objects;
using UnityEngine;

namespace Core.Objects {
    public class Bullet : Ammo<BulletConfig>, IBullet {

        public override float Radius => data.colliderRadius;

        private float lifetime;

        public void Fire() {
            lifetime = data.lifetime;
        }

        private void Update() {
            transform.Translate(direction * (data.speed * Time.deltaTime));

            if ((lifetime -= Time.deltaTime) <= 0) Reset();
        }

    }
}