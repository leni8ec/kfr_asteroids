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


        private void InternalReset() {
            State.Reset();
            Reset();
        }

        public override void Destroy() {
            base.Destroy();
            InternalReset();
        }


        /// <summary>
        /// Called when object is initialized.
        /// <para>When data (State and Config) - is set.</para>
        /// </summary>
        protected abstract void Initialize();
    }
}