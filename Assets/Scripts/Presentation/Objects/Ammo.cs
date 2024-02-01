using UnityEngine;

namespace Presentation.Objects {
    public abstract class Ammo<T> : Entity<T> where T : ScriptableObject, new() {
        protected Vector2 direction;

        public void Set(Vector2 startPoint, Vector2 direction) {
            transform.position = startPoint;
            this.direction = direction;
        }

    }
}