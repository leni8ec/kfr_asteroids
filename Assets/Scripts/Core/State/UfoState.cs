﻿using Core.Interface.Containers;
using Core.State.Base;
using UnityEngine;

namespace Core.State {
    public class UfoState : EntityState, IDirectionContainer {

        public bool huntState;
        public float huntCountdown;

        public Transform target;
        public Vector3 Direction { get; set; }


        protected override void OnReset() {
            huntState = default;
            huntCountdown = default;
            target = default;
            Direction = default;
        }

    }
}