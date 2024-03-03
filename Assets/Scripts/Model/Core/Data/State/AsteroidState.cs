using Model.Core.Data.Containers;
using Model.Core.Data.State.Base;
using UnityEngine;

namespace Model.Core.Data.State {
    public class AsteroidState : EntityState, IDirectionContainer {

        public Vector3 Direction { get; set; }

        protected override void OnReset() {
            Direction = default;
        }

    }
}