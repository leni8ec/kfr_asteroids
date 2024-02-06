using Core.Interface.Objects;
using UnityEngine;

namespace Core.Objects.Base {
    public abstract class EntityBase : MonoBehaviour, IEntity {

        public GameObject GameObject { get; private set; }
        public Transform Transform { get; private set; }

        protected virtual void Awake() {
            Transform = transform;
            GameObject = gameObject;
        }


        public delegate void DisposeEvent(EntityBase entity);
        public event DisposeEvent Dispose;

        public virtual void Destroy() {
            Dispose?.Invoke(this);
        }

        public abstract void Reset();
    }
}