using Core.Interface.Containers;
using Core.Interface.Objects;
using Core.Interface.View;
using Core.Objects.Base;
using Core.State.Base;
using UnityEngine;

namespace Presentation.Objects.Base {
    public abstract class EntityView<TEntity, TState, TConfig> : MonoBehaviour, IEntityView
        where TEntity : Entity<TState, TConfig>, new()
        where TState : EntityState, new()
        where TConfig : ScriptableObject {

        // Sugar
        protected TState State => data.State;
        protected TConfig Config => data.Config;

        private IDataContainer<TState, TConfig> data; //    use internal only
        public TEntity Entity { get; private set; } //      use in 'View'
        public IEntity EntityLink { get; private set; } //  use in 'Core'
        public GameObject GameObject { get; private set; }
        public Transform Transform { get; private set; }

        protected virtual void Awake() {
            Transform = transform;
            GameObject = gameObject;

            // Create new Entity object
            TEntity entity = new();
            EntityLink = entity;
            Entity = entity;

            data = entity;

            // Set Unity object required data to state
            data.State.GameObject = GameObject;
            data.State.Transform = transform;
        }

    }


}