using Core.Interface.Config;
using Core.Interface.Objects;
using Core.State.Base;
using UnityEngine;

namespace Core.Objects.Base {
    public abstract class ColliderEntity<TState, TConfig> : Entity<TState, TConfig>, ICollider
        where TState : class, IStateData, new()
        where TConfig : ScriptableObject, IColliderRadiusContainer {

        public float ColliderRadius => Config.ColliderRadius;
        public Vector3 Pos => transform.position;

    }
}