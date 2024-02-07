using System;
using Core.Config;
using Core.Interface.Base;
using Core.Interface.Objects;
using Core.Objects.Base;
using Core.State;

namespace Core.Objects {
    public class Bullet : Ammo<BulletAmmoState, BulletConfig>, IBullet {

        public event Action FireEvent;

        public override void Reset() { }

        protected override void Initialize() { }

        public void Fire() {
            State.lifetime = Config.lifetime;
            FireEvent?.Invoke();
        }

        public override void Upd(float deltaTime) {
            Transform.Translate(State.Direction * (Config.speed * deltaTime));

            if ((State.lifetime -= deltaTime) <= 0) Destroy();
        }
    }
}