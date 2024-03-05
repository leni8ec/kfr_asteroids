using Model.Data.Reactive;
using UnityEngine;

namespace Model.Data.State.Base {
    public abstract class EntityState : IStateData {

        /// <summary>
        /// Entity active state (`true` by default)
        /// </summary>
        public ReactiveProperty<bool> Active { get; } = new(true);

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