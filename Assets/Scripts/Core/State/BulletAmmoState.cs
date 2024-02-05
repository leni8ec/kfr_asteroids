using Core.Interface.Config;
using Core.State.Base;
using UnityEngine;

namespace Core.State {
    public class BulletAmmoState : IStateData, IDirectionContainer {

        public float lifetime;
        public Vector3 Direction { get; set; }

        public void Reset() {
            lifetime = default;
            Direction = default;
        }

    }
}