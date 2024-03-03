using System;
using Model.Data.State;
using Model.Data.Unity.Config;
using Model.Entity.Base;
using Model.Entity.Interface;

namespace Model.Entity {
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