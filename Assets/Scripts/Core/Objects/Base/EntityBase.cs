using Core.Interface.Objects;
using UnityEngine;

namespace Core.Objects.Base {
    public abstract class EntityBase : IEntity {

        // Sugar
        public abstract Transform Transform { get; }
        public abstract GameObject GameObject { get; }

        public delegate void DisposeEvent(EntityBase entity);
        public event DisposeEvent Dispose;

        public virtual void Destroy() {
            Dispose?.Invoke(this);
        }

        public abstract void Reset();

        public abstract void Upd(float deltaTime);
    }
}