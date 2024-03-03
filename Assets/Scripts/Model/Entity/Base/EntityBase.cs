using Model.Entity.Interface;
using UnityEngine;

namespace Model.Entity.Base {
    public abstract class EntityBase : IEntity {

        // Sugar
        public abstract Transform Transform { get; }

        public delegate void CreateEventHandler(EntityBase entity);
        public delegate void DestroyEventHandler();

        public static event CreateEventHandler StaticCreateEvent;
        public event DestroyEventHandler DestroyEvent;


        protected void CreateInternal() {
            StaticCreateEvent?.Invoke(this);
            OnCreate();
        }

        public virtual void Destroy() {
            OnDestroy(); // Callback called first (before state reset)
            DestroyEvent?.Invoke();
            Reset(); // Call reset after Destroy event
        }

        public abstract void Reset();


        /// <summary>
        /// Called when entity is created.
        /// <para>After data (state and config) - is set.</para>
        /// </summary>
        protected virtual void OnCreate() { }

        /// <summary>
        /// Called on destroy entity (before reset)
        /// </summary>
        protected virtual void OnDestroy() { }

        /// <summary>
        /// Called after Destroy and after return entity to pool
        /// </summary>
        protected virtual void OnReset() { }


        public abstract void Upd(float deltaTime);
    }
}