using UnityEngine;

namespace Core.Objects {
    public abstract class Ammo<T> : Entity<T> where T : ScriptableObject, new() {
        protected Vector3 direction;

        public virtual void Set(Vector3 startPoint, Vector3 direction) {
            transform.position = startPoint;
            this.direction = direction;
        }

    }
}