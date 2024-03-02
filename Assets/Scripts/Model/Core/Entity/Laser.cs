using System;
using Model.Core.Data.State;
using Model.Core.Data.Unity.Config;
using Model.Core.Entity.Base;
using Model.Core.Interface.Entity;

namespace Model.Core.Entity {
    public class Laser : Ammo<LaserAmmoState, LaserConfig>, ILaser {

        public float MaxDistance => Config.maxDistance;

        public event Action FireEvent;


        public void Fire() {
            State.duration = Config.duration;
            Transform.up = State.Direction;

            FireEvent?.Invoke();
        }

        public override void Upd(float deltaTime) {
            if ((State.duration -= deltaTime) < 0) {
                Destroy();
            }
        }

    }
}