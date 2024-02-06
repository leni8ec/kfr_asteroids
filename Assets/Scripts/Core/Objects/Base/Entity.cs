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


        public virtual void Reset() {
            State.Reset();
        }

        public override void Destroy() {
            base.Destroy();
            Reset();
        }

        /// <summary>
        /// Called when the Data (State and Config) - is set
        /// </summary>
        protected abstract void Initialize();

    }
}