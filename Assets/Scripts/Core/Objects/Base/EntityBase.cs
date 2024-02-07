using Core.Interface.Objects;

namespace Core.Objects.Base {
    public abstract class EntityBase : IEntity {

        public delegate void DisposeEvent(EntityBase entity);
        public event DisposeEvent Dispose;

        public virtual void Destroy() {
            Dispose?.Invoke(this);
        }

        public abstract void Reset();

        public abstract void Upd(float deltaTime);
    }
}