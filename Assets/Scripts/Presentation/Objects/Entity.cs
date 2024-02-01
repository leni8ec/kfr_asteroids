using Framework.Objects;
using UnityEngine;

namespace Presentation.Objects {
    public abstract class Entity<T> : EntityBase, IEntity where T : ScriptableObject, new() {

        public delegate void DisposeEvent(Entity<T> entity);
        public event DisposeEvent Dispose;

        protected T data;

        public void SetData(T data) {
            this.data = data;
        }

        public abstract float Radius { get; }
        public Vector2 Pos => transform.position;

        public virtual void Reset() {
            Dispose?.Invoke(this);
        }

        public virtual void Destroy() {
            Reset();
        }

    }
}