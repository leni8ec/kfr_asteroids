using Model.Core.Data.Containers;
using Model.Core.Data.State.Base;
using UnityEngine;

namespace Model.Core.Data.State {
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