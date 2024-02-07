using System;
using Model.Core.Config;
using Model.Core.Interface.Objects;
using Model.Core.Objects.Base;
using Model.Core.State;

namespace Model.Core.Objects {
    public class Bullet : Ammo<BulletAmmoState, BulletConfig>, IBullet {

        public event Action FireEvent;

        protected override void OnReset() { }

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