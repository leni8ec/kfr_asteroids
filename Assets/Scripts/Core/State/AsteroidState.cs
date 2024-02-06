using Core.Interface.Config;
using Core.Interface.Containers;
using Core.Interface.State;
using Core.State.Base;
using UnityEngine;

namespace Core.State {
    public class AsteroidState : EntityState, IDirectionContainer {

        public Vector3 Direction { get; set; }

        public override void Reset() {
            Direction = default;
        }

    }
}