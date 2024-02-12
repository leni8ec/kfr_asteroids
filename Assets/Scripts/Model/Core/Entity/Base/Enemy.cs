using Model.Core.Data.State.Base;
using Model.Core.Interface.Config;
using Model.Core.Interface.Containers;
using UnityEngine;

namespace Model.Core.Entity.Base {
    public abstract class Enemy<TState, TConfig> : ColliderEntity<TState, TConfig>
        where TState : EntityState, IDirectionContainer, new()
        where TConfig : IConfigData, IColliderRadiusContainer {

        public virtual void Set(Vector3 direction) {
            State.Direction = direction;
        }

    }
}