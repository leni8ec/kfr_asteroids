using Model.Core.Interface.Containers;
using Model.Core.State.Base;
using UnityEngine;

namespace Model.Core.State {
    public class AsteroidState : EntityState, IDirectionContainer {

        public Vector3 Direction { get; set; }

        protected override void OnReset() {
            Direction = default;
        }

    }
}