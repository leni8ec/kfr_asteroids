using Model.Core.Interface.State;
using UnityEngine;

namespace Model.Core.Data.State.Base {
    public abstract class EntityState : IStateData {

        /// <summary>
        /// Entity active state (don't reset value in 'Reset')
        /// </summary>
        public ValueChange<bool> Active { get; } = new();

        public Transform Transform { get; set; }

        public void Reset() {
            Transform.position = default;
            Transform.eulerAngles = default;
            Transform.localScale = Vector3.one;

            // Active.Reset(); - Don't 'reset' Active value!

            OnReset();
        }

        protected abstract void OnReset();
    }
}