using Core.Config;
using Core.Interface.Objects;
using Core.State;
using UnityEngine;

namespace Core.Objects {
    public class Bullet : Ammo<BulletAmmoState, BulletConfig>, IBullet {

        protected override void Initialize() { }

        public void Fire() {
            State.lifetime = Config.lifetime;
        }

        private void Update() {
            transform.Translate(State.Direction * (Config.speed * Time.deltaTime));

            if ((State.lifetime -= Time.deltaTime) <= 0) Destroy();
        }

    }
}