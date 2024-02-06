using Core.Interface.Containers;
using Core.Interface.Objects;
using Core.Interface.View;
using Core.Objects.Base;
using Core.State.Base;
using UnityEngine;

namespace Presentation.Objects {
    public abstract class EntityView<TEntity, TState, TConfig> : MonoBehaviour, IEntityView
        where TEntity : Entity<TState, TConfig>, new()
        where TState : EntityState, new()
        where TConfig : ScriptableObject {

        private IDataContainer<TState, TConfig> data;
        // Sugar
        protected TState State => data.State;
        protected TConfig Config => data.Config;

        public IEntity Entity { get; private set; }
        public GameObject GameObject { get; private set; }
        public Transform Transform { get; private set; }

        protected virtual void Awake() {
            Transform = transform;
            GameObject = gameObject;

            // Create new Entity object
            TEntity entity = new TEntity();
            data = entity;
            Entity = entity;
        }

    }


}