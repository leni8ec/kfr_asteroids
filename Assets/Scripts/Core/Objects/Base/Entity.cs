using Core.Interface.Containers;
using Core.State.Base;
using UnityEngine;

namespace Core.Objects.Base {
    public abstract class Entity<TState, TConfig> : EntityBase, IDataContainer<TState, TConfig>
        where TState : EntityState, new()
        where TConfig : ScriptableObject {

        public TState State { get; } = new();
        public TConfig Config { get; private set; }

        // Sugar
        public override Transform Transform => State.Transform;
        public override GameObject GameObject => State.GameObject;

        public void SetConfig(TConfig config) {
            Config = config;

            Initialize();
        }

        public sealed override void Reset() {
            State.Reset();
            OnReset();
        }

        protected abstract void OnReset();

        public override void Destroy() {
            base.Destroy();
            Reset();
        }


        /// <summary>
        /// Called when object is initialized.
        /// <para>When data (State and Config) - is set.</para>
        /// </summary>
        protected abstract void Initialize();
    }
}