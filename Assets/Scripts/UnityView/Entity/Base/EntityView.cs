using Model.Core.Data.State.Base;
using Model.Core.Data.Unity.Config.Base;
using Model.Core.Entity.Base;
using Model.Core.Entity.Interface;
using UnityEngine;

namespace UnityView.Entity.Base {
    public abstract class EntityView<TEntity, TState, TConfig> : MonoBehaviour, IEntityView
        where TEntity : Entity<TState, TConfig>, new()
        where TState : EntityState, new()
        where TConfig : IConfigData {

        protected TEntity Entity { get; private set; } //      use in 'View'
        private IEntityDataContainer<TState, TConfig> Data => Entity; //    use internal only

        public GameObject GameObject { get; private set; }
        public Transform Transform { get; private set; }

        // Sugar
        protected TState State => Data.State;
        protected TConfig Config => Data.Config;


        protected virtual void Awake() {
            Transform = transform;
            GameObject = gameObject;
        }

        /// <summary>
        /// Called when entity is created.
        /// <para>- After entity data (state and config) - is set.</para>
        /// <para>- After 'Awake' and before 'Start'</para>
        /// </summary>
        public void Create(IEntity entity) {
            Entity = (TEntity) entity;
            State.Transform = Transform;

            // Game object state (active by default)
            State.Active.Value = true;
            State.Active.Changed += active => GameObject.SetActive(active);

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