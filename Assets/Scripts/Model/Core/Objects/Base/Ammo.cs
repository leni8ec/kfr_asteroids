﻿using Model.Core.Interface.Containers;
using Model.Core.State.Base;
using UnityEngine;

namespace Model.Core.Objects.Base {
    public abstract class Ammo<TState, TConfig> : ColliderEntity<TState, TConfig>
        where TState : EntityState, IDirectionContainer, new()
        where TConfig : ScriptableObject, IColliderRadiusContainer, new() {

        public Vector3 Direction => State.Direction;

        public virtual void Set(Vector3 startPoint, Vector3 direction) {
            Transform.position = startPoint;
            State.Direction = direction;
        }

    }
}