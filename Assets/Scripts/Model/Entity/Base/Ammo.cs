using Model.Data.Containers;
using Model.Data.State.Base;
using Model.Data.Unity.Config.Base;
using UnityEngine;

namespace Model.Entity.Base {
    public abstract class Ammo<TState, TConfig> : ColliderEntity<TState, TConfig>
        where TState : EntityState, IDirectionContainer, new()
        where TConfig : IConfigData, IColliderRadiusContainer, new() {

        public Vector3 Direction => State.Direction;

        public virtual void Set(Vector3 startPoint, Vector3 direction) {
            Transform.position = startPoint;
            State.Direction = direction;
        }

    }
}