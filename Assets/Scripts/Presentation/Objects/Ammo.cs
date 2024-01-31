using UnityEngine;

namespace Presentation.Objects {
    public abstract class Ammo<T> : Entity<T> where T : ScriptableObject {
        public Vector2 startPoint;
        public Vector2 direction;

        public void Set(Vector2 startPoint, Vector2 direction) {
            this.startPoint = startPoint;
            this.direction = direction;
        }


    }
}