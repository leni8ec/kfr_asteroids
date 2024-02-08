using Model.Core.Data.State.Base;
using Model.Core.Interface.Containers;
using UnityEngine;

namespace Model.Core.Data.State {
    public class BulletAmmoState : EntityState, IDirectionContainer {

        public float lifetime;
        public Vector3 Direction { get; set; }

        protected override void OnReset() {
            lifetime = default;
            Direction = default;
        }

    }
}