using Model.Core.Interface.Containers;
using Model.Core.State.Base;
using UnityEngine;

namespace Model.Core.Objects.Base {
    public abstract class Enemy<TState, TConfig> : ColliderEntity<TState, TConfig>
        where TState : EntityState, IDirectionContainer, new()
        where TConfig : ScriptableObject, IColliderRadiusContainer {

        public virtual void Set(Vector3 direction) {
            State.Direction = direction;
        }

    }
}