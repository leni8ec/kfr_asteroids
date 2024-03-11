using Model.Entity.Interface;
using UnityEngine;

namespace Model.Entity.Base {
    public abstract class EntityBase : IEntity {

        /// <summary>
        /// The world space position
        /// </summary>
        public abstract Vector3 Position { get; set; }
        /// <summary>
        /// The world space rotation as Euler angles in degrees
        /// </summary>
        public abstract Vector3 Rotation { get; set; }
        /// <summary>
        /// The world space forward direction of entity (transform.up)
        /// </summary>
        public abstract Vector3 Forward { get; }

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