using Model.Core.Interface.Containers;
using Model.Core.State.Base;
using UnityEngine;

namespace Model.Core.State {
    public class LaserAmmoState : EntityState, IDirectionContainer {

        public float duration;
        public Vector3 Direction { get; set; }

        protected override void OnReset() {
            duration = default;
            Direction = default;
        }

    }
}