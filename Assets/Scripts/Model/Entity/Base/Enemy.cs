using Model.Data.Containers;
using Model.Data.State.Base;
using Model.Data.Unity.Config.Base;
using UnityEngine;

namespace Model.Entity.Base {
    public abstract class Enemy<TState, TConfig> : ColliderEntity<TState, TConfig>
        where TState : EntityState, IDirectionContainer, new()
        where TConfig : IConfigData, IColliderRadiusContainer {

        public virtual void Set(Vector3 direction) {
            State.Direction = direction;
        }

    }
}