using Core.Interface.Containers;
using Core.State.Base;
using UnityEngine;

namespace Core.State {
    public class LaserAmmoState : EntityState, IDirectionContainer {

        public float duration;
        public Vector3 Direction { get; set; }

        public override void Reset() {
            duration = default;
            Direction = default;
        }

    }
}