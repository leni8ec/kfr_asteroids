using Core.Interface.Config;
using Core.Interface.Containers;
using Core.Interface.State;
using UnityEngine;

namespace Core.State {
    public class AsteroidState : IStateData, IDirectionContainer {

        public Vector3 Direction { get; set; }

        public void Reset() {
            Direction = default;
        }

    }
}