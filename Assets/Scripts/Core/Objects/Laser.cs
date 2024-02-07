using System;
using Core.Config;
using Core.Interface.Objects;
using Core.Objects.Base;
using Core.State;

namespace Core.Objects {
    public class Laser : Ammo<LaserAmmoState, LaserConfig>, ILaser {

        public float MaxDistance => Config.maxDistance;

        public event Action FireEvent;


        public override void Reset() { }

        protected override void Initialize() { }

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