using Core.Interface.Objects;
using UnityEngine;

namespace Core.Objects {
    public abstract class EntityBase : MonoBehaviour, IEntity {

        public delegate void DisposeEvent(EntityBase entity);
        public event DisposeEvent Dispose;

        public virtual void Destroy() {
            Dispose?.Invoke(this);
        }

    }
}