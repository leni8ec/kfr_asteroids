using Model.Core.Interface.Objects;
using UnityEngine;

namespace Model.Core.Objects.Base {
    public abstract class EntityBase : IEntity {

        // Sugar
        public abstract Transform Transform { get; }
        public abstract GameObject GameObject { get; }

        public delegate void CreateEventHandler(EntityBase entity);
        public delegate void DisposeEventHandler();

        public static event CreateEventHandler StaticCreateEvent;
        public event DisposeEventHandler DestroyEvent;


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
        /// Called when object is initialized.
        /// <para>When data (State and Config) - is set.</para>
        /// </summary>
        protected virtual void OnCreate() { }

        /// <summary>
        /// Called on destroy object (before reset)
        /// </summary>
        protected virtual void OnDestroy() { }

        /// <summary>
        /// Called after Destroy and after return object to pool
        /// </summary>
        protected virtual void OnReset() { }


        public abstract void Upd(float deltaTime);
    }
}