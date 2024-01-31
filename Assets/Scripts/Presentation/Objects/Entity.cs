using Framework.Objects;
using UnityEngine;

namespace Presentation.Objects {
    public abstract class Entity<T> : EntityBase, IEntity where T : ScriptableObject, new() {

        public delegate void Dispose(Entity<T> entity);
        public Dispose dispose;

        protected T data;

        public void SetData(T data) {
            this.data = data;
        }

        public abstract float Radius { get; }
        public Vector2 Pos => transform.position;

        public virtual void Reset() {
            dispose(this);
        }

    }
}