using Core.Interface.Config;
using Core.State.Base;
using UnityEngine;

namespace Core.State {
    public class UfoState : IStateData, IDirectionContainer {

        public bool huntState;
        public float huntCountdown;

        public Transform target;
        public Vector3 Direction { get; set; }


        public void Reset() {
            huntState = default;
            huntCountdown = default;
            target = default;
            Direction = default;
        }

    }
}