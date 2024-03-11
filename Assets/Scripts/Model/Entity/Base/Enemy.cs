using Model.Data.Containers;
using Model.Data.State.Base;
using Model.Data.Unity.Config.Base;
using UnityEngine;

namespace Model.Entity.Base {
    public abstract class Enemy<TState, TConfig> : ColliderEntity<TState, TConfig>
        where TState : EntityState, IDirectionContainer, new()
        where TConfig : IConfigData, IColliderRadiusContainer {

        public void Init(Vector3 position, Vector3 direction) {
            State.Transform.position = position;
            State.Direction = direction;
        }

    }
}