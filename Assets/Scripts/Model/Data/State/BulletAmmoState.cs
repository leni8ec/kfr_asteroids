using Model.Data.Containers;
using Model.Data.State.Base;
using UnityEngine;

namespace Model.Data.State {
    public class BulletAmmoState : EntityState, IDirectionContainer {

        public float lifetime;
        public Vector3 Direction { get; set; }

        protected override void OnReset() {
            lifetime = default;
            Direction = default;
        }

    }
}