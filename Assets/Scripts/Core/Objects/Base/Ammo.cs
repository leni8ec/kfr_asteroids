using Core.Interface.Containers;
using Core.Interface.State;
using UnityEngine;

namespace Core.Objects.Base {
    public abstract class Ammo<TState, TConfig> : ColliderEntity<TState, TConfig>
        where TState : class, IStateData, IDirectionContainer, new()
        where TConfig : ScriptableObject, IColliderRadiusContainer, new() {

        public Vector3 Direction => State.Direction;

        public virtual void Set(Vector3 startPoint, Vector3 direction) {
            transform.position = startPoint;
            State.Direction = direction;
        }

    }
}