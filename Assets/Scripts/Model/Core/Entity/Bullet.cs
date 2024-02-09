using System;
using Model.Core.Data.State;
using Model.Core.Entity.Base;
using Model.Core.Interface.Entity;
using Model.Core.Unity.Data.Config;

namespace Model.Core.Entity {
    public class Bullet : Ammo<BulletAmmoState, BulletConfig>, IBullet {

        public event Action FireEvent;


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