using Core.Config;
using Core.Interface.Objects;
using UnityEngine;

namespace Core.Objects {
    public class Bullet : Ammo<BulletConfig>, IBullet {

        public override float Radius => config.colliderRadius;

        private float lifetime;

        public void Fire() {
            lifetime = config.lifetime;
        }

        private void Update() {
            transform.Translate(direction * (config.speed * Time.deltaTime));

            if ((lifetime -= Time.deltaTime) <= 0) Reset();
        }

    }
}