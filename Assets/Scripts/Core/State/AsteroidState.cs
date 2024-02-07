using Core.Interface.Containers;
using Core.State.Base;
using UnityEngine;

namespace Core.State {
    public class AsteroidState : EntityState, IDirectionContainer {

        public Vector3 Direction { get; set; }

        protected override void OnReset() {
            Direction = default;
        }

    }
}