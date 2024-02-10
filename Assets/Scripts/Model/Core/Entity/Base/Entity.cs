﻿using Model.Core.Data.State.Base;
using Model.Core.Interface.Containers;
using UnityEngine;

namespace Model.Core.Entity.Base {
    public abstract class Entity<TState, TConfig> : EntityBase, IDataContainer<TState, TConfig>
        where TState : EntityState, new()
        where TConfig : ScriptableObject {

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