using Model.Data.Containers;
using Model.Data.State.Base;
using Model.Entity.Base;
using UnityEngine;

namespace Model.Data.State {
    public class UfoState : EntityState, IDirectionContainer {

        public bool huntState;
        public float huntCountdown;

        public EntityBase target;
        public Vector3 Direction { get; set; }


        protected override void OnReset() {
            huntState = default;
            huntCountdown = default;
            target = default;
            Direction = default;
        }

    }
}