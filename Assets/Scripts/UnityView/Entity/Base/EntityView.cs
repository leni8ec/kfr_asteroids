using Model.Core.Data.State.Base;
using Model.Core.Entity.Base;
using Model.Core.Interface.Containers;
using Model.Core.Interface.Entity;
using Model.Core.Interface.View;
using UnityEngine;

namespace UnityView.Entity.Base {
    public abstract class EntityView<TEntity, TState, TConfig> : MonoBehaviour, IEntityView
        where TEntity : Entity<TState, TConfig>, new()
        where TState : EntityState, new()
        where TConfig : ScriptableObject {

        protected TEntity Entity { get; private set; } //      use in 'View'
        private IDataContainer<TState, TConfig> Data => Entity; //    use internal only

        public GameObject GameObject { get; private set; }
        public Transform Transform { get; private set; }

        // Sugar
        protected TState State => Data.State;
        protected TConfig Config => Data.Config;


        protected virtual void Awake() {
            Transform = transform;
            GameObject = gameObject;
        }

        public void Create(IEntity entity) {
            Entity = (TEntity)entity;
            State.GameObject = GameObject;
            State.Transform = Transform;
            SubscribeEvents();
            OnCreate();
        }


        /// <summary>
        /// Subscribe for entity events and data changes (called after 'Awake' and before 'OnCreate', 'Start')
        /// </summary>
        protected abstract void SubscribeEvents();

        /// <summary>
        /// Called when Entity is created (called after 'Awake', 'SubscribeEvents' and before 'Start')
        /// </summary>
        protected virtual void OnCreate() { }

    }
}