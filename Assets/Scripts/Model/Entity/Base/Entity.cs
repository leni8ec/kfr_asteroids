using Model.Data.State.Base;
using Model.Data.Unity.Config.Base;
using UnityEngine;

namespace Model.Entity.Base {
    public abstract class Entity<TState, TConfig> : EntityBase, IEntityDataContainer<TState, TConfig>
        where TState : EntityState, new()
        where TConfig : IConfigData {

        public TState State { get; } = new();
        public TConfig Config { get; private set; }

        // Sugar
        public override Transform Transform => State.Transform;

        public void Create(TConfig config) {
            Config = config;
            CreateInternal();
        }

        public sealed override void Reset() {
            State.Reset();
            OnReset();
        }

    }
}