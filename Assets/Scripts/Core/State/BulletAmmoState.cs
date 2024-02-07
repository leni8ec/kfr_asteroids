using Core.Interface.Containers;
using Core.State.Base;
using UnityEngine;

namespace Core.State {
    public class BulletAmmoState : EntityState, IDirectionContainer {

        public float lifetime;
        public Vector3 Direction { get; set; }

        public override void Reset() {
            lifetime = default;
            Direction = default;
        }

    }
}