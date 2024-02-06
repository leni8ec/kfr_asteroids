using Core.Interface.Config;
using Core.Interface.Containers;
using Core.Interface.State;
using UnityEngine;

namespace Core.State {
    public class LaserAmmoState : IStateData, IDirectionContainer {

        public float duration;
        public Vector3 Direction { get; set; }

        public void Reset() {
            duration = default;
            Direction = default;
        }

    }
}