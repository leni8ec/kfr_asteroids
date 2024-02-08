﻿using Model.Core.Data.State.Base;
using Model.Core.Interface.Containers;
using UnityEngine;

namespace Model.Core.Data.State {
    public class LaserAmmoState : EntityState, IDirectionContainer {

        public float duration;
        public Vector3 Direction { get; set; }

        protected override void OnReset() {
            duration = default;
            Direction = default;
        }

    }
}