using Core.Interface.State;
using UnityEngine;

namespace Core.Objects.Base {
    public abstract class Entity<TState, TConfig> : EntityBase
        where TState : class, IStateData, new()
        where TConfig : ScriptableObject {

        protected TConfig Config { get; private set; }
        protected TState State { get; private set; }

        public void SetData(TState state, TConfig config) {
            State = state;
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