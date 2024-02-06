using Core.Interface.State;
using UnityEngine;

namespace Core.State.Base {
    public abstract class EntityState : IStateData {

        public GameObject GameObject { get; set; }
        public Transform Transform { get; set; }

        public virtual void Reset() {
            Transform.position = default;
            Transform.eulerAngles = default;
            Transform.localScale = default;
        }
    }
}