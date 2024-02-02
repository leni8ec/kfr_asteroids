using UnityEngine;

namespace Core.Objects {
    public abstract class Enemy<T> : Entity<T> where T : ScriptableObject, new() {
        protected Vector3 direction;

        public virtual void Set(Vector3 direction) {
            this.direction = direction;
        }

    }
}