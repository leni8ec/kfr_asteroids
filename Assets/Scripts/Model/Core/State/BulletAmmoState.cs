using Model.Core.Interface.Containers;
using Model.Core.State.Base;
using UnityEngine;

namespace Model.Core.State {
    public class BulletAmmoState : EntityState, IDirectionContainer {

        public float lifetime;
        public Vector3 Direction { get; set; }

        protected override void OnReset() {
            lifetime = default;
            Direction = default;
        }

    }
}