using Core.Interface.Containers;
using Core.Interface.State;
using UnityEngine;

namespace Core.Objects.Base {
    public abstract class Enemy<TState, TConfig> : ColliderEntity<TState, TConfig>
        where TState : class, IStateData, IDirectionContainer, new()
        where TConfig : ScriptableObject, IColliderRadiusContainer {

        public virtual void Set(Vector3 direction) {
            State.Direction = direction;
        }

    }
}