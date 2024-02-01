using UnityEngine;

namespace Presentation.Objects {
    public abstract class Enemy<T> : Entity<T> where T : ScriptableObject, new() {
        protected Vector3 direction;

        public void Set(Vector3 direction) {
            this.direction = direction;
        }
        
    }
}