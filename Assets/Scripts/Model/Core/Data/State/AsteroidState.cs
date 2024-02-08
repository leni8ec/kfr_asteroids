using Model.Core.Data.State.Base;
using Model.Core.Interface.Containers;
using UnityEngine;

namespace Model.Core.Data.State {
    public class AsteroidState : EntityState, IDirectionContainer {

        public Vector3 Direction { get; set; }

        protected override void OnReset() {
            Direction = default;
        }

    }
}