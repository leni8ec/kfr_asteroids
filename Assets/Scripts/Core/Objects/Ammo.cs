using UnityEngine;

namespace Core.Objects {
    public abstract class Ammo<T> : Entity<T> where T : ScriptableObject, new() {
        protected Vector2 direction;

        public virtual void Set(Vector2 startPoint, Vector2 direction) {
            transform.position = startPoint;
            this.direction = direction;
        }

    }
}