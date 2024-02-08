using System;
using Model.Core.Data.State;
using Model.Core.Interface.Objects;
using Model.Core.Objects.Game.Base;
using Model.Core.Unity.Data.Config;

namespace Model.Core.Objects.Game {
    public class Laser : Ammo<LaserAmmoState, LaserConfig>, ILaser {

        public float MaxDistance => Config.maxDistance;

        public event Action FireEvent;


        protected override void OnReset() { }

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