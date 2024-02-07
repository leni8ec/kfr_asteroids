﻿using System;
using Model.Core.Config;
using Model.Core.Interface.Objects;
using Model.Core.Objects.Base;
using Model.Core.State;

namespace Model.Core.Objects {
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