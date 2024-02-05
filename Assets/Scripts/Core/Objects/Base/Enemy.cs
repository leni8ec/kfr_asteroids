using Core.Interface.Config;
using Core.Objects.Base;
using Core.State.Base;
using UnityEngine;

namespace Core.Objects {
    public abstract class Enemy<TState, TConfig> : ColliderEntity<TState, TConfig>
        where TState : class, IStateData, IDirectionContainer, new()
        where TConfig : ScriptableObject, IColliderRadiusContainer {

        public virtual void Set(Vector3 direction) {
            State.Direction = direction;
        }

    }
}