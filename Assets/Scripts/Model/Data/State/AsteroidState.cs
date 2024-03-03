using Model.Data.Containers;
using Model.Data.State.Base;
using UnityEngine;

namespace Model.Data.State {
    public class AsteroidState : EntityState, IDirectionContainer {

        public Vector3 Direction { get; set; }

        protected override void OnReset() {
            Direction = default;
        }

    }
}