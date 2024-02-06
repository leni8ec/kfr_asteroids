using Core.Config;
using Core.Interface.Objects;
using Core.Objects.Base;
using Core.State;
using UnityEngine;

namespace Core.Objects {
    public class Bullet : Ammo<BulletAmmoState, BulletConfig>, IBullet {

        protected override void Initialize() { }

        public override void Reset() { }

        public void Fire() {
            State.lifetime = Config.lifetime;
        }

        private void Update() {
            Transform.Translate(State.Direction * (Config.speed * Time.deltaTime));

            if ((State.lifetime -= Time.deltaTime) <= 0) Destroy();
        }

    }
}