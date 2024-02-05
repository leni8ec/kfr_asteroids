using Core.Interface.Config;
using Core.State.Base;
using UnityEngine;

namespace Core.State {
    public class AsteroidState : IStateData, IDirectionContainer {

        public Vector3 Direction { get; set; }

        public void Reset() {
            Direction = default;
        }

    }
}