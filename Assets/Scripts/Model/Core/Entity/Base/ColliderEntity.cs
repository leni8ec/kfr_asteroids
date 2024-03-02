using Model.Core.Data.State.Base;
using Model.Core.Data.Unity.Config.Base;
using Model.Core.Interface.Containers;
using Model.Core.Interface.Entity;
using UnityEngine;

namespace Model.Core.Entity.Base {
    public abstract class ColliderEntity<TState, TConfig> : Entity<TState, TConfig>, ICollider
        where TState : EntityState, new()
        where TConfig : IConfigData, IColliderRadiusContainer {

        public float ColliderRadius => Config.ColliderRadius;
        public Vector3 Pos => Transform.position;

    }
}