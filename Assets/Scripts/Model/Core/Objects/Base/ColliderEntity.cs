using Model.Core.Data.State.Base;
using Model.Core.Interface.Containers;
using Model.Core.Interface.Objects;
using UnityEngine;

namespace Model.Core.Objects.Base {
    public abstract class ColliderEntity<TState, TConfig> : Entity<TState, TConfig>, ICollider
        where TState : EntityState, new()
        where TConfig : ScriptableObject, IColliderRadiusContainer {

        public float ColliderRadius => Config.ColliderRadius;
        public Vector3 Pos => State.Transform.position;

    }
}