using Model.Core.Interface.State;
using UnityEngine;

namespace Model.Core.Data.State.Base {
    public abstract class EntityState : IStateData {

        public ValueChange<bool> Active { get; } = new();
        public Transform Transform { get; set; }

        public void Reset() {
            Transform.position = default;
            Transform.eulerAngles = default;
            Transform.localScale = Vector3.one;

            OnReset();
        }

        protected abstract void OnReset();
    }
}